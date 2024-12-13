using BarcodeLibrary;

namespace Showcase_L2
{
    public interface IShowcase<T> where T : IProduct
    {
        T this[int index] { get; set; }

        int Id { get; set; }
        int Length { get; }
        void OrderById();
        void OrderByName();
        void Pop();
        void Pop(int index);
        void Push(T product);
        void Push(T product, int index);
        void Rearrage(int index1st, int index2nd);
        void Replace(int indexFrom, int indexTo);
        int SearchPositionById(int id);
        int SearchPositionByName(string name);
        void UpdateProductId();
        void UpdateProductId(int index);
    }
}