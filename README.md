# yazilim-mimarisi-ve-tasarimi
Observer Design Pattern

Observer adı üzerinde gözlemci, izleyici, gözcü yahut gözetmen diye nitelendirilen, anlamı gibi işlev gören bir tasarım desenidir. Observer ile mevcut nesnenin durumunda herhangi bir değişiklik olduğunda, bu değişiklerden diğer nesneleri haberdar eden bir tasarımdan bahsediyoruz. Dahada net bir şekilde bahsetmek gerekirse, elimizdeki “x” nesnesinin “y” özelliğinde bir güncellenme, değişiklik yahut belirli bir şartın gerçekleşmesi gibi bir durum da  bu “x” nesnesini -izleyen- -gözleyen- diğer “z”, “w”, “k” vs. nesnelerine bu yeni durumu bildiren sisteme Observer tasarım deseni diyoruz. Örneğin bir alışveriş sitesinde bir ürüne indirim yapıldığında kullanıcılarınıza e-mail ile haber verilir iken bu kalıp kullanılabilir. Ya da en basitinden facebook da bir gruba üyesiniz grupta bildirimleri açtığınızda size (ve daha birçok kişiye) gelecek olan bildirim bu yapı ile olabilir.

Şimdi örnek kodumuza geçecek olursak. Elimizde bir Öğrenci nesnesi olsun. Bu Öğrenciyi takip eden; Anne, Baba ve Öğretmen nesneleri olsun. Okul, Öğrenci dersi astığı zaman onu takip eden Annesine, Babasına ve Öğretmenine direkt olarak bu durumu haber veriyor olsun.

İşte bu örnek olay tam tamına Observer tasarım desenine uygun bir örnektir. 

Observer Design Pattern’de

Subject:

Takip edilecek nesneyi Subject terimiyle ifade etmekteyiz. Yukarıdaki örnek olayda Öğrenci nesnesi bizim Subject’timiz olmaktadır.

Observer:

Subject’i takip edecek olan aboneler tek tip olmayabilirler. Yukarıdaki örnek olayımızda Subject’i, hem Ana, hem Baba hemde Öğretmen nesneleri takip etmektedirler. İşte böyle bir durumda birden fazla tipe arayüz görevi görecek abstract yahut interface yapımıza Observer denmektedir. Anlayacağınız örnek olayımızda Öğrenci nesnemizi takip edenlerin birden fazla tip olmasını sağlayan o sistem Observer arayüzüdür.

Concrete:

Subject’i takip eden nesnelerdir. Yukarıdaki örnek olayımızda; Anne, Baba ve Öğretmen nesneleri Concrete nesneleridir


![](https://github.com/RukiyeSahin/yazilim-mimarisi-ve-tasarimi/blob/master/Observer.png)

Diyagramda gördüğünüz gibi Subject(Hedef) olarak belirtilen nesnemize Observer’lar(Gözlemciler) subscribe yani abone olmuşlardır. Subject nesnemiz içerisinde herhangi bir “y” özelliği güncellendiğinde Notify metodu tetiklenecek ve bu metod Subject’e abone olan tüm Observer’ların Update metodunu çalıştıracaktır. Şimdi bu olayın kod kısmına geçersek.

Öncelikle Subject’imize(yani hedefimize) bir çok tipin abone olabilmesini sağlayacak Observer arayüzünü geliştirelim.

```sh
abstract public class Observer
{ 
  public override void Update(); 
}
 ```
    
Observer sınıfından kalıtım alan her abone sınıfa Update metodu uygulanacaktır. Haliyle Subject’imizde ilgili alan değiştiğinde abonelerimizdeki Update metodu tetiklenecektir.

Şimdi Concrete nesnelerimizi başka bir deyişle takip edecek sınıflarımızı oluşturalım.

```sh
public class BabaObserver: Observer
{ 
  public override void Update()
  {
     Console.WriteLine("Öğrencinin kaçtığından babasının haberi oldu.");
  }
 }  
```

```sh
public class AnneObserver: Observer
{ 
  public override void Update()
  {
     Console.WriteLine("Öğrencinin kaçtığından annesinin haberi oldu.");
  }
 }  
```

```sh
public class OgretmenObserver: Observer
{ 
  public override void Update()
  {
     Console.WriteLine("Öğrencinin kaçtığından öğretmeninin haberi oldu.");
  }
 }  
```

Şimdide Subject'imizi yani takip edilecek nesnemizi inşa edelim.

```sh
public class Ogrenci
{
  public string Adi {get; set;}
  public string SoyAdi {get; set;}
  public string Memleket {get; set;}
  public int Sinif {get; set;}
  bool dersiAstimi;
  //Bu property Observer D.P.'de değeri kontrol edilen özelliğimizdir.
  //set bloğuna dikkat edersek eğer set edilen değer true ise
  //Notify metodu tetiklenmekte ve tüm abonelere haber gönderilmektedir
  public bool dersiAstiMi{
   get {return dersiAstiMi; }
   set{ 
     if(value = true){
      Notify();
      dersiAstiMi = value;
      }else
       dersiAstiMi = value;
       }
  }
  //Subject nesnesi kendisine obone olan gözlemcileri bu koleksiyonda tutacaktır.
  List<Observer>Gozlemciler;
  public Ogrenci()
  {
    this.Gozlemciler = new List<Observer>();
  }
  //Gözlemci Ekle 
  public void AboneEkle(Observer observer)
  { 
    Gozlemciler.Add(observer);
  }
  //Gözlemci Çıkar
  public void AboneCıkar(Observer observer)
  { 
    Gozlemciler.Remove(observer);
  }
  //Herhangi bir güncelleme olursa ilgili gözlemcilere haber verecek metodumuzdur.
  public void Nofity(){
    Gozlemciler.ForEach(g=>
                          { 
                            g.Update();
                          }
  }
 } 
 ```
 
 
 Ve son olarak main fonksiyonunu yazalım.
 ```sh
 class Program
  {
    public static void Main(sting[] args)
    {
     Ogrenci o = new Ogrenci();
     o.AboneEkle(new BabaObserver());
     o.AboneEkle(new AnneObserver());
     o.AboneEkle(new OgretmenObserver());
     
     o.Adi = "Rukiye";
     o.SoyAdi ="Sahin";
     o.Memleket = "Konya"
     o.Sinif =202;
     o.DersiAstiMi =true;
     Console.ReadKey(true);
    }
   }
  ```
  
 DersiAstiMi propertysine "true" değerini set ettiği satırdan sonra Observer patterndeki istediğimiz sonucu almaktayız.
 

