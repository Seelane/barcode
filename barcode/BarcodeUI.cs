using System;
using Showcase_L2;
using BarcodeLibrary;
using System.Diagnostics.Metrics;
using System.Diagnostics;
using System.Drawing;
using System.Xml.Linq;

namespace barcode
{
    public class BarcodeUI
    {
        private static bool InputAndConvertToBoolean()
        {
            bool result;
            bool success;
            while (true)
            {
                success = Boolean.TryParse(Console.ReadLine(), out result);
                if (success)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Try again.");
                }
            }
        }
        private static double InputAndConvertToDouble()
        {
            double result;
            bool success;
            while (true)
            {
                success = Double.TryParse(Console.ReadLine(), out result);
                if (success)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Try again.");
                }
            }
        }
        private static int InputAndConvertToInt32()
        {
            int result;
            bool success;
            while (true)
            {
                success = Int32.TryParse(Console.ReadLine(), out result);
                if (success)
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Try again.");
                }
            }
        }
        /*
        функционал программы:

        штрихкод:
            - создание штрихкода

        витрина
            - создание витрины
            - добавление товара в витрину
            - смена ID витрины
            - смена ID товара на витрине
            - смена позиции товара на витрине
            - сортировка товаров в витрине
            - поиск товара на витрине
            - удаление товара из витрины

        товар:
            - создание товара
            - смена ID товара
            - изменение товара
            - помещение товара в хранилище
            - извлечение товара из хранилища на витрину
         */
        public static void UserInterface()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Hello There! It's Lab 5!");
                Console.WriteLine("This program allows you to work with barcodes and showcases.");
                Console.Write("Choose what you want to do:\n" +
                    "1 - work with a barcode,\n" +
                    "2 - work with a showcase.\n" +
                    "Your choice: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.Clear();
                        BarcodeInterface();
                        break;
                    case "2":
                        Console.Clear();
                        ShowcaseInterface();
                        break;
                    default:
                        Console.WriteLine("Try again.");
                        break;
                }
            }
        }
        /*
 функционал программы:
штрихкод:
    - создание штрихкода
*/
        public static void BarcodeInterface()
        {
            Console.Clear();
            Console.WriteLine("Barcode mode selected.");
            Console.WriteLine("Input \"q\" to return to the main menu" +
                "\nInput any key to create a barcode.");
            switch (Console.ReadLine())
            {
                case "q":
                    return;
                default:
                    CreateBarcode();
                    break;
            }
        }
        public static void CreateBarcode()
        {
            Console.Clear();
            Console.WriteLine("".PadLeft(80, '='));
            BarcodeRecord barcode = new("inputText");
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Input text you want convert to barcode:");
                var inputText = Console.ReadLine();

                switch (inputText)
                {
                    case null:
                    case "":
                    case "\n":
                        break;
                    default:
                        barcode.Text = inputText;
                        break;
                }

                Console.Write("Select the operating mode:\n1 - display text," +
                    "\n2 - display barcode," +
                    "\n3 - display full barcode." +
                    "\nYour choice: ");
                var selectedMode = Console.ReadLine();
                switch (selectedMode)
                {
                    case "1":
                    case "text":
                    case "display text":
                        IBarcode.BarcodeType = BarcodeType.Text;
                        Console.WriteLine("Text only:");
                        break;
                    case "2":
                    case "barcode":
                    case "display barcode":
                        IBarcode.BarcodeType = BarcodeType.Barcode;
                        Console.WriteLine("Barcode only:");
                        break;
                    case "3":
                    case "full":
                    case "display full":
                    case "full barcode":
                    case "display full barcode":
                    default:
                        IBarcode.BarcodeType = BarcodeType.Full;
                        Console.WriteLine("Barcode with text:");
                        break;
                }
                if (barcode != null)
                    Console.WriteLine(barcode);
                else
                    Console.WriteLine("Error: your text is invalid.");

                Console.WriteLine("Input \"q\" to return to the main menu" +
                    "\nInput any key to create another barcode.");
                switch (Console.ReadLine())
                {
                    case "q":
                        return;
                    default:
                        break;
                }
            }
        }


        public static void ShowcaseInterface()
        {
            var showcaseList = new List<IShowcase<IProduct>>();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Showcase mode selected.");
                Console.WriteLine("Input \"q\" to return to the main menu" +
                    "\nInput \"w\" to work with a created showcases" +
                    "\nPress any key to create a new showcase.");

                switch (Console.ReadLine())
                {
                    case "q":
                        return;
                    case "w":
                        var showcase1 = (Showcase<IProduct>)(7, 3);

                        var lab4Data = new List<IProduct>
    {
        new Printer(3000, "ВОЙНА И МИР III", "Л.Н. Толстой",true, 1867, 300000),
        new Printer(1000, "ВОЙНА И МИР I", "Л.Н. Толстой", false, 1863, 1000000),
        new Printer(2000, "ВОЙНА И МИР II", "Л.Н. Толстой", false, 1865, 200000),
        new Printer(5555, "Хранители", "С. Маккоуд", true, 2008, 2071),
        new InkjetPrinter(6666, "Понимание комикса", "А. Шпигельман", true, 1990, 860, "base", 255)
    };
                        foreach (var товар in lab4Data)
                        {
                            showcase1.Push(товар);
                        }
                        showcaseList.Add(showcase1);
                        break;
                    default:
                        IShowcase<IProduct> showcase = CreateShowcase();
                        if (showcase == null) continue;
                        showcaseList.Add(showcase);

                        break;
                }
                Console.Clear();
                Console.WriteLine("Available showcases:");
                if (showcaseList.Count == 0)
                {
                    Console.WriteLine("No showcases created yet.");
                    Console.WriteLine("You should create a new showcase." +
                        "\nPress any key to continue");
                    Console.ReadKey();
                    IShowcase<IProduct> showcase = CreateShowcase();
                    if (showcase == null) continue;
                    showcaseList.Add(showcase);
                    Console.WriteLine("Available showcases:");
                }
                foreach (var item in showcaseList)
                {
                    Console.WriteLine($"{showcaseList.IndexOf(item)}) Showcase {showcaseList.IndexOf(item)}: ID: {item.Id}, Capacity: {item.Length}.");
                }
                Console.WriteLine("Input any key to work with a showcase" +
                    "\nInput \"c\" to create a new showcase " +
                    "\nInput \"q\" return to the main menu");
                Console.Write("Your choice: ");
                int showcaseNumber;
                switch (Console.ReadLine())
                {
                    case "c":
                        IShowcase<IProduct> showcase = CreateShowcase();
                        if (showcase == null) continue;
                        showcaseList.Add(showcase);
                        continue;
                    case "q":
                        return;
                    default:
                        Console.Write("Input number of showcase: ");
                        showcaseNumber = InputAndConvertToInt32();
                        if (showcaseNumber < 0) Console.WriteLine("Input number incorrect.");
                        if (showcaseNumber > showcaseList.Count - 1) Console.WriteLine("There is no such showcase.");
                        break;
                }
                Console.Clear();
                Console.WriteLine($"Work with Showcase {showcaseNumber}");
                Console.WriteLine("".PadLeft(80, '='));
                Console.WriteLine("Input \"q\" to return to the main menu" +
                    "\nInput \"1\" to add a product to the showcase." + "\nInput \"2\" to remove a product from the showcase." +
                    "\nInput \"3\" to change the ID of the showcase." +
                    "\nInput \"4\" to change the ID of the product." +
                    "\nInput \"5\" to sort products in the showcase." +
                    "\nInput \"6\" to search for a product in the showcase." +
                    "\nInput \"7\" to remove all products from the showcase." +
                    "\nInput any key to continue.");
                switch (Console.ReadLine())
                {
                    case "q":
                        return;
                    case "1":
                        PushProductInShowcase(showcaseList[showcaseNumber], CreateProduct());
                        break;
                    case "2":
                        PopProductFromShowcase(showcaseList[showcaseNumber]);
                        break;
                    case "3":
                        ChangeShowcaseId(showcaseList[showcaseNumber]);
                        break;
                    case "4":
                        ChangeProductIdInShowcase(showcaseList[showcaseNumber]);
                        break;
                    case "5":
                        SortProductsInShowcase(showcaseList[showcaseNumber]);
                        break;
                    case "6":
                        SearchProductInShowcase(showcaseList[showcaseNumber]);
                        break;
                }
            }
        }

        /*
функционал программы:
витрина
- создание витрины
- добавление товара в витрину
- смена ID витрины
- смена ID товара на витрине
- смена позиции товара на витрине
- сортировка товаров в витрине
- поиск товара на витрине
- удаление товара из витрины
*/
        private static void ChangeShowcaseId(IShowcase<IProduct> showcase)
        {
            var a = showcase;
            Console.WriteLine("Input new ID of showcase: ");
            a.Id = InputAndConvertToInt32();
            Console.WriteLine(a);
            Console.ReadKey();
        }
        private static void ChangeProductIdInShowcase(IShowcase<IProduct> showcase)
        {
            var a = showcase;
            Console.WriteLine(a);
            Console.Write("Input \"q\" to return to the main menu" +
                "\nInput any key to change the ID of the product: ");
            int number;
            switch (Console.ReadLine())
            {
                case "q":
                    return;
                default:
                    Console.WriteLine("Input position of product: ");
                    number = InputAndConvertToInt32();
                    if (a[number] == null)
                    {
                        Console.WriteLine("There is no such product.");
                        break;
                    }
                    Console.WriteLine("Input new ID of product: ");
                    a[number].Id = InputAndConvertToInt32();
                    Console.WriteLine(a);
                    break;
            }
            Console.WriteLine(a);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        private static void SearchProductInShowcase(IShowcase<IProduct> showcase)
        {
            IShowcase<IProduct> a = showcase;
            Console.WriteLine(a);
            Console.Write("Input \"q\" to return to the main menu" +
                "Input name of product to find: ");
            int key = -1;
            string name = Console.ReadLine();
            switch (name)
            {
                case "q":
                    return;
                default:
                    key = a.SearchPositionByName(name);
                    break;
            }
            if (key != -1)
            {
                Console.WriteLine($"Product found at position {key}.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        private static void SortProductsInShowcase(IShowcase<IProduct> showcase)
        {
            IShowcase<IProduct> a = showcase;
            a.OrderByName();
            Console.WriteLine(a);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        private static void PushProductInShowcase(IShowcase<IProduct> showcase, IProduct product)
        {
            var a = showcase;
            var p = product;
            a.Push(p);
            Console.Clear();
            Console.WriteLine(a);
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }
        private static void PopProductFromShowcase(IShowcase<IProduct> showcase)
        {
            IShowcase<IProduct> a = showcase;
            Console.WriteLine(a);
            Console.Write("Input \"q\" to return to the main menu" +
                "\nInput number of product to remove: ");
            int number;
            switch (Console.ReadLine())
            {
                case "q":
                    return;
                default:
                    number = InputAndConvertToInt32();
                    a.Pop(number);
                    break;
            }
            Console.Clear();
            Console.WriteLine("Оставшиеся элементы: ");
            Console.WriteLine(a);
            Console.ReadKey();
        }

        private static IShowcase<IProduct> CreateShowcase()
        {
            IShowcase<IProduct> showcase = null;
            Console.Clear();
            Console.WriteLine("Showcase Creating");
            Console.WriteLine("".PadLeft(80, '='));
            Console.Write("Input Showcase capacity: ");
            int capacity = InputAndConvertToInt32();
            if (capacity < 0)
            {
                Console.WriteLine("Capacity is incorrect.");
                return null;
            }
            Console.Write("Input Showcase ID: ");
            int id = InputAndConvertToInt32();
            if (id < 0)
            {
                Console.WriteLine("ID is incorrect.");
                return null;
            }
            showcase = (Showcase<IProduct>)(capacity, id);
            Console.WriteLine($"Showcase was created with ID = {id} and capacity = {capacity}.");
            Console.ReadKey();
            return showcase;
        }
        /*
        функционал программы:
        товар:
            - создание товара
            - смена ID товара
            - изменение товара
            - помещение товара в хранилище
            - извлечение товара из хранилища на витрину
         */
        private static void ProductInterface()
        {
            var productList = new List<IProduct>();
            IProduct product = null;
            Console.Clear(); Console.WriteLine("Product mode selected.");
            Console.WriteLine("Input \"q\" to return to the main menu" +
                "\nInput \"1\" to create a product.");
            switch (Console.ReadLine())
            {
                case "q":
                    return;
                case "1":
                    product = CreateProduct();
                    if (product == null) return;
                    productList.Add(product);
                    break;
                default:
                    break;
            }
            Console.WriteLine("Input \"q\" to return to the main menu" +
                "\nInput \"p\" to print created products.");
            switch (Console.ReadLine())
            {
                case "q":
                    return;
                case "p":
                    foreach (var item in productList)
                        Console.WriteLine(item);
                    break;
                default:
                    break;
            }
        }
        private static IProduct CreateProduct()
        {
            Console.Clear();
            Console.WriteLine("Product Creating");
            Console.WriteLine("".PadLeft(80, '='));
            IProduct product = null;
            Console.WriteLine("You can create the following types of product:" +
                "\n1 - Printer" +
                "\n2 - Inkjet Printer" +
                "\nEnter the type number of the product." +
                "\nInput \"q\" return to the main menu.");
            switch (Console.ReadLine())
            {
                case "1":
                    product = CreateProductPrinter();
                    break;
                case "2":
                    product = CreateProductInkjetPrinter();
                    break;
                case "q":
                    return null;
                default:
                    break;
            }
            return product;
        }
        private static Printer CreateProductPrinter()
        {
            int id = 0;
            string name = "";
            string description = "";
            bool isColor = false;
            double price = 0.0;
            int count = 0;

            Printer printer = null;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Заполните данные о продукте:");
                Console.Write("Id: ");
                id = InputAndConvertToInt32();
                if (id < 0)
                    break;
                Console.Write("Name: ");
                name = Console.ReadLine();
                Console.Write("Description: ");
                description = Console.ReadLine();
                Console.Write("Is color: ");
                isColor = InputAndConvertToBoolean();
                Console.Write("Price: ");
                price = InputAndConvertToDouble();
                if (price < 0)
                    break; Console.Write("Count: ");
                count = InputAndConvertToInt32();
                if (count < 0)
                    break;
                break;
            }
            printer = new Printer(id, name, description, isColor, price, count);
            return printer;
        }

        private static Printer CreateProductInkjetPrinter()
        {
            InkjetPrinter printer = null;
            //var tmp = CreateProductPrinter();
            int id = 0;
            string name = "";
            string description = "";
            bool isColor = false;
            double price = 0.0;
            int count = 0;
            string inkType = "";
            int numberOfColors = 0;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Заполните данные о продукте:");
                Console.Write("Id: ");
                id = InputAndConvertToInt32();
                if (id < 0)
                    break;
                Console.Write("Name: ");
                name = Console.ReadLine();
                Console.Write("Description: ");
                description = Console.ReadLine();
                Console.Write("Is color: ");
                isColor = InputAndConvertToBoolean();
                Console.Write("Price: ");
                price = InputAndConvertToDouble();
                if (price < 0)
                    break; Console.Write("Count: ");
                count = InputAndConvertToInt32();
                if (count < 0)
                    break;
                Console.Write("Ink type: ");
                inkType = Console.ReadLine();
                Console.Write("Number of colors: ");
                numberOfColors = InputAndConvertToInt32();
                if (numberOfColors < 0) break;
                break;
            }
            printer = new InkjetPrinter(id, name, description, isColor, price, count, inkType, numberOfColors);
            return printer;
        }
    }
}

