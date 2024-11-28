using System;
using Showcase_L2;
using BarcodeLibrary;
using System.Security.Cryptography.X509Certificates;

public static class Program
{
    static void Main(string[] args)
    {
        //    IBarcode.BarcodeType = BarcodeType.Full;

        while (true)
        {
            Console.WriteLine("Какое задание желаете проверить?");
            string input = Console.ReadLine();
            switch (input)
            {
                case "1":
                    TestLab1();
                    break;
                case "2":
                    TestLab2();
                    break;
                case "3":
                    TestLab3();
                    break;
                case "4":
                    Console.WriteLine("В разработке...");
                    break;
                default:
                    Console.WriteLine("Try again.");
                    break;
            }
            Console.WriteLine("Очистить консоль? 1 - да, 2 - нет");
            input = Console.ReadLine();
            if (input == "1")
                Console.Clear();
            else if (input != "2")
                break;
            else Console.WriteLine("Начнём с начала");
        }
    }

    static void TestLab3()
    {
        Console.Clear();
     
        var lab3Data = new List<IProduct>
        {
            new Printer(3000, "ВОЙНА И МИР III", "Л.Н. Толстой", true, 1867, 300000),
            new Printer(1000, "ВОЙНА И МИР I", "Л.Н. Толстой", false, 1863, 1000000),
            new Printer(2000, "ВОЙНА И МИР II", "Л.Н. Толстой", true, 1865, 200000)
        };

        var lab3Data2 = new List<InkjetPrinter>
        {
            new (5555, "Хранители", "С. МакКоуд", true, 2008, 2071, "base", 255),
            new (6666, "Понимание комикса", "А. Шпигельман", true, 1990, 860, "base", 1488)
        };

        IShowcase<IProduct> a1 = (Showcase<IProduct>)(7, 1);  // витрина на 7 позиций с 1м идентификатором
        IShowcase<InkjetPrinter> a2 = (Showcase<InkjetPrinter>)(3, 10);  // витрина на 3 позиции с 10м идентификатором

        foreach (var товар in lab3Data)
        {
            a1.Push(товар);
        }
        foreach (var товар in lab3Data2)
        {
            a1.Push(товар);
        }
        a1.OrderByName();

        var sample1 = new InkjetPrinter(7777, "Ходячие мертвецы", "Р. Кирман", true, 2003, 2257, "not base", 256);
        var sample2 = new Printer(4000, "ВОЙНА И МИР IV", "Л.Н. Толстой", false, 1869, 400000);

        a2[0] = sample1;
        a1[5] = a2[0];
        a1[6] = sample2;

        a1.Id = 2; // смена ID витрины
        sample1.Id++; // смена ID товара
        sample2.Id++; // смена ID товара, будет перезаписан штрихкод
        
        // Каждый товар должен правильно отображать штрих код:
        // - ОбычныйТовар на витрине отображает штрихкод с позицией и ID витрины
        // - КонечныйТовар отображает только ID товара, обрамленный *
        Console.WriteLine(a1);

        Console.WriteLine("Cringeee");

        a2[0] = (InkjetPrinter)a1[5]; // вам надо поменять этот код, чтобы можно было запустить терминал (менять только здесь)
        Console.WriteLine(a2);
        sample1.Id = 123;
        Console.WriteLine(sample1);
    }

    static void TestLab2()
    {
        Console.WriteLine("".PadLeft(80, '='));
        Showcase<IProduct> showcase = 5; // Способ создания витрины на 5 мест
        var sample = new InkjetPrinter(1111, "ВОЙНА И МИРЪ I", "Л.Н. Толстой", true, 1863, 1000000, "base", 255);
        Console.WriteLine(sample);
        sample.Barcode.Text = "0000";
        Console.WriteLine(sample);
        showcase.Push(sample);
        Console.WriteLine(showcase[0]);
        var lab2Data = new List<Product> {
                    new Printer(3000, "ВОЙНА И МИРЪ III", "Л.Н. Толстой", false, 1867, 300000),
                    new Printer(2000, "ВОЙНА И МИРЪ II", "Л.Н. Толстой", false, 1865, 200000),
                    new Printer(4000, "ВОЙНА И МИРЪ IV", "Л.Н. Толстой", true, 1869, 400000)
                };

        foreach (var product in lab2Data)
            showcase.Push(product);
        showcase.Push(sample); // допускаемый способ закидывания товара на витрину в пустое место
        showcase[4] = sample; //прямая отправка товара на нужную позицию витрины
        Console.WriteLine("Вывести sample? 1 - да, 2 - нет");
        if (Console.ReadLine() == "1")
        {
            Console.WriteLine(sample);
        }
        else { Console.WriteLine("Пропускаем"); }
        sample.Id++; // Можно изменять идентификатор, при этом на витрине товар утратит свой уникальный штрих код
        Console.WriteLine("А теперь?");
        if (Console.ReadLine() == "1")
            Console.WriteLine(sample);

        showcase.OrderByName(); // Обязательная сортировка, она же может чинить штрихкоды товаров,
        Console.WriteLine("Вывести 5 элемент витрины? 1 - да, 2 - нет");
        if (Console.ReadLine() == "1")
            Console.WriteLine(showcase[4]);// т.к. фактически происходит изменение позиций товаров на витрине.
        showcase.Id++;          // можно менять ID витрины
        showcase.OrderById();
        Console.WriteLine("Вывести 5 элемент витрины? 1 - да, 2 - нет");
        if (Console.ReadLine() == "1")
            Console.WriteLine(showcase[4]);
        Console.WriteLine("".PadLeft(40, '='));
        Console.WriteLine("Вывести всю витрину? 1 - да, 2 - нет");
        if (Console.ReadLine() == "1")
            Console.WriteLine(showcase); // приведение к строке витрины
        showcase.Replace(4, 1);
        showcase.Push(sample, 1);

        Console.WriteLine("".PadLeft(20, '='));
        Console.WriteLine("Вывести всю витрину? 1 - да, 2 - нет");
        if (Console.ReadLine() == "1") Console.WriteLine(showcase);
        int searchingResult;
        searchingResult = showcase.SearchPositionById(1112);
        {
            if (searchingResult != -1)
                Console.WriteLine("Товар найден");
            else Console.WriteLine("Товар не найден");
        }

        searchingResult = showcase.SearchPositionByName("ВОЙНА И МИРЪ III");
        {
            if (searchingResult != -1)
                Console.WriteLine("Товар найден.");
            else Console.WriteLine("Товар не найден.");
        }
    }

    static void TestLab1()
    {
        BarcodeRecord barcode = new("inputText");

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

        Console.WriteLine("Select the operating mode:\n1 - display text,\n2 - display barcode,\n3 - display full barcode");
        var selectedMode = Console.ReadLine();
        switch (selectedMode)
        {
            case "1":
            case "text":
            case "display text":
                IBarcode.BarcodeType = BarcodeType.Text;
                Console.WriteLine($"Text only:");
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
        {
            Console.WriteLine(barcode);
        }
        else
        {
            Console.WriteLine("Error: your text is invalid.");
        }
    }


    static void DisplayMessage (string message)
    {
        Console.WriteLine(message);
    }
}
