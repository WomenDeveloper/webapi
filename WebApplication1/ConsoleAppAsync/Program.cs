void Metod1()
{
    Console.WriteLine("metod1... basla");
    Thread.Sleep(4000);
    Console.WriteLine("metod1... bitis");
}

void Metod2()
{
    Console.WriteLine("metod2... basla");
    Thread.Sleep(7000);
    Console.WriteLine("metod2... bitis");
}

void Islem1()
{
    DateTime d1, d2;
    TimeSpan fark;
    d1 = DateTime.Now;
    Metod1();
    Metod2();
    d2 = DateTime.Now;
    fark = d2 - d1;
    Console.WriteLine(fark.TotalMilliseconds);

}

//void den Task'a cevrildi...2.ornek için
async Task Metod1Async()
{
    Console.WriteLine("metod1... basla");
    await Task.Delay(4000);
    Console.WriteLine("metod1... bitis");
}

//void den Task'a cevrildi...2. ornek için
async Task Metod2Async()
{
    Console.WriteLine("metod2... basla");
    await Task.Delay(7000);
    Console.WriteLine("metod2... bitis");
}

//void Islem2Async()
//{
//    //Metod1 ve Metod2 tetiklenir fakat Islem2 metodların bitisini beklemez...

//    DateTime d1, d2;
//    TimeSpan fark;
//    d1 = DateTime.Now;
//    Metod1Async();
//    Metod2Async();
//    d2 = DateTime.Now;
//    fark = d2 - d1;
//    Console.WriteLine(fark.TotalMilliseconds);

//}

async Task Islem2Async()
{
    DateTime d1, d2;
    TimeSpan fark;
    d1 = DateTime.Now;
    //await Metod1Async();
    //await Metod2Async();

    await Task.WhenAll(Metod1Async(), Metod2Async());

    d2 = DateTime.Now;
    fark = d2 - d1;
    Console.WriteLine(fark.TotalMilliseconds);

}


//Asenkron Programlama
//Bir metodun içerisinde await kullanılacaksa, metod async olarak işaretlenmeli...



//Islem1();
await Islem2Async();