using BarcodeLibrary;
using System;
using System.Text;
namespace Showcase_L2
{
    /// <summary>
    /// витрина
    /// </summary>
    public class Showcase
    {
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

        private readonly Product[] _container;
        /// <summary>
        /// скртытый оператор преобразования типов
        /// </summary>
        public static implicit operator Showcase(int size) => new (size);
        private Showcase(int size)
        {
            _container = new Product[size];
        }

        public Product this[int index]
        {
            get
            {
                if (index >= _container.Length) return null;
                Product p = _container[index];
                _container[index] = null;
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

        public void Push(Product product)
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

        public void Push(Product product, int index)
        {
            if (index >= _container.Length) { Console.WriteLine("Ты кринж"); return; }
            this[index] = product;
        }

        public void Pop()
        {
            for (int i = _container.Length - 1; i >= 0; i--)
            {
                if (_container[i] != null)
                {
                    this[i] = null;
                    return;
                }
            }
        }

        public void Pop(int index)
        {
            if (index >= _container.Length) { Console.WriteLine("Ты кринж"); return; }
            this[index] = null;
        }

        public void Replace(int indexFrom, int indexTo)
        {
            if (indexFrom >= _container.Length || indexTo >= _container.Length) { Console.WriteLine("Ты кринж"); return; }
            this[indexTo] = this[indexFrom];
        }

        public void Rearrage(int index1st, int index2nd)
        {
            if (index1st >= _container.Length || index2nd >= _container.Length) { Console.WriteLine("Ты кринж"); return; }
            Product temp = this[index1st];
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
