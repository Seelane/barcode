using System;
using Showcase_L2;
using BarcodeLibrary;
using System.Security.Cryptography.X509Certificates;

public static class Program
{
    static void Main(string[] args)
    {
        IBarcode.BarcodeType = BarcodeType.Full;

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
                    Console.WriteLine("В разработке...");
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

    //static void TestLab3()
    //{
    //    Console.Clear();
    //    var lab3Data = new List<Товароподобный>
    //    {
    //        new ОбычныйТовар(3000, "ВОЙНА И МИР III", "Л.Н. Толстой", 1867, 300000),
    //        new ОбычныйТовар(1000, "ВОЙНА И МИР I", "Л.Н. Толстой", 1863, 1000000),
    //        new ОбычныйТовар(2000, "ВОЙНА И МИР II", "Л.Н. Толстой", 1865, 200000)
    //    };

    //    var lab3Data2 = new List<КонечныйТовар>
    //    {
    //        new КонечныйТовар(5555, "Хранители", "С. МакКоуд", 2008, 2071),
    //        new КонечныйТовар(6666, "Понимание комикса", "А. Шпигельман", 1990, 860)
    //    };

    //    ВитринаТовароподобных a1 = new ВитринаТовароподобных(7, 1);  // витрина на 7 позиций с 1м идентификатором
    //    ВитринаКонечныхТоваров a2 = new ВитринаКонечныхТоваров(3, 10);  // витрина на 3 позиции с 10м идентификатором

    //    foreach (var товар in lab3Data)
    //    {
    //        a1.Push(товар);
    //    }
    //    foreach (var товар in lab3Data2)
    //    {
    //        a1.Push(товар);
    //    }
    //    a1.OrderByName();

    //    var sample1 = new КонечныйТовар(7777, "Ходячие мертвецы", "Р. Кирман", 2003, 2257);
    //    var sample2 = new ОбычныйТовар(4000, "ВОЙНА И МИР IV", "Л.Н. Толстой", 1869, 400000);

    //    a2[0] = sample1;
    //    a1[5] = a2[0];
    //    a1[6] = sample2;

    //    a1.Id = 2; // смена ID витрины
    //    sample1.Id++; // смена ID товара
    //    sample2.Id++; // смена ID товара, будет перезаписан штрихкод

    //    // Каждый товар должен правильно отображать штрих код:
    //    // - ОбычныйТовар на витрине отображает штрихкод с позицией и ID витрины
    //    // - КонечныйТовар отображает только ID товара, обрамленный *
    //    // - Товар sample2, у него штрих код должен выглядеть так: "* 4001 *"
    //    Console.WriteLine(a1);

    //    a2[0] = a1[5]; // вам надо поменять этот код, чтобы можно было запустить терминал (менять только здесь)
    //    Console.WriteLine(a2);
    //}

    static void TestLab2()
    {
        Console.WriteLine("".PadLeft(80, '='));
        Showcase showcase = 5; // Способ создания витрины на 5 мест
        var sample = new InkjetPrinter(1000, "ВОЙНА И МИРЪ I", "Л.Н. Толстой", true, 1863, 1000000);
        var lab2Data = new List<Product> {
                    new InkjetPrinter(3000, "ВОЙНА И МИРЪ III", "Л.Н. Толстой", false, 1867, 300000),
                    new InkjetPrinter(2000, "ВОЙНА И МИРЪ II", "Л.Н. Толстой", false, 1865, 200000),
                    new InkjetPrinter(4000, "ВОЙНА И МИРЪ IV", "Л.Н. Толстой", true, 1869, 400000)
                };

        foreach (var product in lab2Data)
            showcase.Push(product);
        showcase.Push(sample); // допускаемый способ закидывания товара на витрину в пустое место
        showcase[4] = sample; //прямая отправка товара на нужную позицию витрины
        Console.WriteLine(sample);
        sample.Id++; // Можно изменять идентификатор, при этом на витрине товар утратит свой уникальный штрих код
        Console.WriteLine(sample);
        showcase.OrderByName(); // Обязательная сортировка, она же может чинить штрихкоды товаров,
        Console.WriteLine(showcase[4]);// т.к. фактически происходит изменение позиций товаров на витрине.
        showcase.Id++;          // можно менять ID витрины
        showcase.OrderById();
        Console.WriteLine(showcase[4]);
        Console.WriteLine("".PadLeft(40, '='));
        Console.WriteLine(showcase); // приведение к строке витрины
        showcase.Replace(4, 1);
        showcase.Push(sample, 1);

        Console.WriteLine("".PadLeft(20, '='));
        Console.WriteLine(showcase);
        int iii;
        iii = showcase.SearchPositionById(1001);
        {
            if (iii != -1)
                Console.WriteLine($"Товар найден");
            else Console.WriteLine("Товар не найден");
        }

        iii = showcase.SearchPositionByName("ВОЙНА И МИРЪ III");
        {
            if (iii != -1)
                Console.WriteLine("Товар найден.");
            else Console.WriteLine("Товар не найден.");
        }
    }

    static void TestLab1()
    {
        Barcode barcode = new("inputText");

        Console.WriteLine("Input text you want convert to barcode:");
        barcode.Text = Console.ReadLine();

        Console.WriteLine($"Barcode as string:\n{barcode.BarcodeString}");
        Console.WriteLine($"Barcode text:\n{barcode.Text}");

        if (barcode != null)
        {
            Console.WriteLine("\nCreated barcode:\n");
            Console.WriteLine(barcode);
        }
        else
        {
            Console.WriteLine("Error: your text is invalid.");
        }
    }

}
