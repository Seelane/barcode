using BarcodeLibrary;
using System;
using System.Text;
namespace Showcase_L2
{
    /// <summary>
    /// витрина
    /// </summary>
    public class Showcase<T> where T : IProduct
    {
private const string WARN = "Ты кринж";
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                }
                UpdateProductId();
            }
        }
        private readonly T[] _container;
        /// <summary>
        /// скртытый оператор преобразования типов
        /// </summary>
        public static implicit operator Showcase<T>(int size) => new (size);
        private Showcase(int size)
        {
            _container = new T[size];
        }

        /// <summary>
        /// скртытый оператор преобразования типов с кортежем
        /// </summary>
        public static implicit operator Showcase<T>((int size, int id) shk) => new(shk.size) { Id = shk.id };
        /// <summary>
        /// конструктор с двумя параметрами
        /// </summary>
        /// <param name="size"></param>
        /// <param name="id"></param>
        public Showcase(int size, int id)
        {
            _container = new T[size];
            Id = size;
        }

        public T this[int index]
        {
            get
            {
                if (index >= _container.Length) return default;
                T p = _container[index];
                _container[index] = default;
                return p;
            }
            set
            {
                if (index >= _container.Length) return;
                _container[index] = value;
                UpdateProductId(index);
            }
        }

        public void UpdateProductId()
        {
            for (int i = 0; i < _container.Length; i++)
            {
                if (_container[i] != null)
                    UpdateProductId(i);
            }
        }
        public void UpdateProductId(int index)
        {
            if (_container[index] == null || index < 0 || index > _container.Length)
            {
                Console.WriteLine("Error!!");
                return;
            }
            _container[index].Barcode.Text = $"{_container[index].Id} {Id} {index}";
        }

        public void Push(T product)
        {
            for (int i = 0; i < _container.Length; i++)
            {
                if (_container[i] == null)
                {
                    this[i] = product;
                    return;
                }
            }
        }

        public void Push(T product, int index)
        {
            if (index >= _container.Length) { Console.WriteLine(WARN); return; }
            this[index] = product;
        }

        public void Pop()
        {
            for (int i = _container.Length - 1; i >= 0; i--)
            {
                if (_container[i] != null)
                {
                    this[i] = default;
                    return;
                }
            }
        }

        public void Pop(int index)
        {
            if (index >= _container.Length) { Console.WriteLine(WARN); return; }
            this[index] = default;
        }

        public void Replace(int indexFrom, int indexTo)
        {
            if (indexFrom >= _container.Length || indexTo >= _container.Length) { Console.WriteLine(WARN); return; }
            this[indexTo] = this[indexFrom];
        }

        public void Rearrage(int index1st, int index2nd)
        {
            if (index1st >= _container.Length || index2nd >= _container.Length) { Console.WriteLine(WARN); return; }
            T temp = this[index1st];
            this[index1st] = this[index2nd];
            this[index2nd] = temp;
        }

        public int SearchPositionById(int id)
        {

            for (int i = 0; i < _container.Length; i++)
            {
                if (_container[i] != null && _container[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public int SearchPositionByName(string name)
        {
            for (int i = 0; i < _container.Length; i++)
            {
                if (_container[i] != null && _container[i].Name == name)
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// сортировка по имени
        /// </summary>
        public void OrderByName()
        {
            Array.Sort(_container, (p1, p2) =>
            {
                if (p1 == null) return 1;
                if (p2 == null) return -1;
                return p1.Name.CompareTo(p2.Name);
            });
            UpdateProductId();
        }
        /// <summary>
        /// сортировка по id
        /// </summary>
        public void OrderById()
        {
            Array.Sort(_container, (p1, p2) =>
            {
                if (p1 == null) return 1;
                if (p2 == null) return -1;
                return p1.Id.CompareTo(p2.Id);
            });
            UpdateProductId();
        }

        public override string ToString()
        {
            int i = 0;
            StringBuilder result = new ("");
            foreach (var item in _container)
            {
                if (item != null)
                {

                    result.AppendLine($"Позиция {i}:\n{item}");
                }
                else result.AppendLine($"Позиция {i}:\nПусто");
                ++i;
            }
            return result.ToString();
        }
    }
}
