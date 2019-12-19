using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObserverPattern
{
    abstract public class Observer
    {
        public abstract void Update();
    }
    public class BabaObserver : Observer
    {
        public override void Update()
        {
            Console.WriteLine("Öğrencinin kaçtığından babasının haberi oldu.");
        }
    }
    public class AnneObserver : Observer
    {
        public override void Update()
        {
            Console.WriteLine("Öğrencinin kaçtığından annesinin haberi oldu.");
        }
    }
    /*public class OgretmenObserver : Observer
    {
        public override void Update()
        {
            Console.WriteLine("Öğrencinin kaçtığından öğretmenin haberi oldu.");
        }
    }*/
    public class Ogrenci
    {
        public string Adi { get; set; }
        public string SoyAdi { get; set; }
        public string Memleket { get; set; }
        public int Sinif { get; set; }

        bool dersiAstiMi;
        //Bu property Observer D. P.'de değeri kontrol edilen özelliğimizdir.
        //set bloğuna dikkat edersek eğer set edilen değer true ise
        //Notify metodu tetiklenmekte ve tüm abonelere haber gönderilmektedir.
        public bool DersiAstiMi
        {
            get { return dersiAstiMi; }
            set
            {
                if (value == true)
                {
                    Notify();
                    dersiAstiMi = value;
                }
                else
                    dersiAstiMi = value;
            }
        }
        //Subject nesnesi kendisine abone olan gözlemcileri bu koleksiyonda tutacaktır.
        List<Observer> Gozlemciler;
        public Ogrenci()
        {
            this.Gozlemciler = new List<Observer>();
        }
        //Gözlemci ekle
        public void AboneEkle(Observer observer)
        {
            Gozlemciler.Add(observer);
        }
        //Gözlemci çıkar
        public void AboneCikar(Observer observer)
        {
            Gozlemciler.Remove(observer);
        }
        //Herhangi bir güncelleme olursa ilgili gözlemcilere haber verecek metodumuzdur.
        public void Notify()
        {
            Gozlemciler.ForEach(g =>
            {
                g.Update();
            });
        }
    }
    class Program
     {
    public static void Main(string[] args)
    {

        Ogrenci o = new Ogrenci();
        o.AboneEkle(new BabaObserver());
        o.AboneEkle(new AnneObserver());
        //o.AboneEkle(new OgretmenObserver());

        o.Adi = "Gençay";
        o.SoyAdi = "Yıldız";
        o.Memleket = "Artvin";
        o.Sinif = 24;
        o.DersiAstiMi = true;

        Console.ReadKey(true);
    }

     }
}
