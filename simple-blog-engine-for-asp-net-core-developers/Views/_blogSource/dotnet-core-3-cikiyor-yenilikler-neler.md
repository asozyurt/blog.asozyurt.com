Teknoloji gelişiyor, teknoloji geliştirme araçları da gelişiyor ve bazen sadece haberlerini bile takip etmekte zorlanacağınız bir hıza ulaşıyor.
.Net Core nispeten yeni bir teknoloji ve yeni yeni yaygınlaşıyor diyebiliriz. Ancak gelişim hızı baş döndürüyor.

Ortamlara alışıyoruz ve en etkili şekilde kullanmaya çalışıyoruz. Yeni özelliklerden de işimize yarayanı alıp yolumuza devam edeceğiz. 

Microsoft konuyla ilgili uzunca bir blog girmiş, ben de önemli gördüğüm bölümleri burada özet olarak anlatmaya çalışacağım.

##### Windows Desktop Desteği

.Net Core un bundan önceki versiyonlarında ana hedef web uygulamaları ve web api ları idi. Bu versiyon ile birlikte Desktop desteği de eklenmiş oluyor.
Fakat bu yalnızca Windows geliştiricilerine hitap ettiğinden şu ana kadar .NetCore u web projeleri için kullanan bizim gibilerde pek heyecan yaratacağını düşünmüyorum.

Teknolojik bir yenilik olarak tanımlayamayız ama cesur bir adımla WPF, WinForms ve WinUI ın açık kaynak hale geliyor. Microsoft un açık kaynak yazılıma son yıllarda bu denli katkı vermesi sevindirici ve şaşırtıcı gerçekten.

##### Build Sonrası Exe Çıktısı

Şimdiye kadar proje derlenmesi sonrası yalnızca _dll_ üretiliyordu (self-contained harici tiplerde), artık varsayılan olarak tüm uygulamalarda build çıktısına _exe_ ekleniyor.

##### Referanslar Artık Build Klasörüne Kopyalanıyor

Şimdiye kadar yalnızca Publish ile kopyalanan projeye eklediğimiz _dll ler_ artık build ile kopyalanır hale geliyor. Build klasörünü Publish klasörü olarak kullanan arkadaşlar için güzel kolaylık. (Yani duyuyoruz, yaptığımızdan değil)

##### In-Box Json Reader

Bu versiyon ile birlikte **System.Text** namespace i altına **Json.Utf8JsonReader** geliyor. Json.Net in reader ına göre 2 kat daha hızlı olacağı söyleniyor.
İlk aşamada Json okuma (sequential access) desteği sunulacak, sonraki versiyonlarda Json yazma, DOM (random access), poco serializer, poco deserializer eklenecekmiş.

Bizler Newtonsoft ve Json.Net ile devam edeceğiz. Bu güncelleme web server tarafına özelleştirilmiş parser ve deserializer yazanlara hitap ediyor.

##### Index ve Range Sınıfları

Tatlı bir özellik. C# 8.0 ın yeniliklerinden bir tanesi.

İsimleri gayet açık ama kısaca bahsedelim. Genellikle _int_ olarak tuttuğumuz index değerlerini artık **Index** tipinde tutabiliyoruz, dizinin başından veya sonundan başlayacak şekilde index tanımı yapabiliyoruz.

^ ile index değerinin sondan itibaren başlandığını belirtebiliyoruz.

<script src="https://gist.github.com/richlander/d2ed10abc144a53ba31989e75c77bc12.js"></script>

Range ise iki Index kullanarak dizinin belirli bir kısmını ifade edebileceğiniz bir yapı. slice işlemi için örnek aşağıdaki gibi. a dizisi içerisinde i1 ve i2 Index leri arasını range ile alabiliyoruz. 

<script src="https://gist.github.com/richlander/e303027e4f2f9c3139286c2cc40e9add.js"></script>

##### Tiered Compilation

.NetCore 2.1 ile gelen ancak performans sorunları nedeniyle 2.2 de varsayılan olmaktan çıkarılan bu özellik şimdi tekrar varsayılan hale geliyor. Yapılan testler başlangıç zamanı ve uygulama performansının önemli ölçüde artırıldığını gösteriyor.

![İstatistik](https://msdnshared.blob.core.windows.net/media/2018/08/asp_net_startup.png)
![İstatistik](https://msdnshared.blob.core.windows.net/media/2018/08/asp_net_steady_state.png)

###### Peki nedir Tiered Compilation?

Tiered Compilation, yazdığınız metotların .Net Framework ü tarafından farklı optimizasyon seviyelerinde derlenmesi demektir. Uygulama açılışında ve uygulama çalışırken metotların en performanslı olabilecek versiyonu Framework tarafından belirlenir ve çalıştırılır.


**Kaynak**

[https://blogs.msdn.microsoft.com/dotnet/2018/12/04/announcing-net-core-3-preview-1-and-open-sourcing-windows-desktop-frameworks/](https://blogs.msdn.microsoft.com/dotnet/2018/12/04/announcing-net-core-3-preview-1-and-open-sourcing-windows-desktop-frameworks/?MC=Vstudio&MC=CSHARP&MC=.NET&MC=CCPLUS&MC=JavaScript)
