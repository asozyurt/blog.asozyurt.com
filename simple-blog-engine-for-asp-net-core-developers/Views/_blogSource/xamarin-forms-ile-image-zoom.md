Merhaba,

Universalist Dergi Mobil çalışmasını __Xamarin__ ile yaptığımı yazdım. O projenin (ilk versiyonunun) en mühim kısmı dergi sayfalarının okuma işlemi. Projenin genel yapısını proje sayfasında detaylı olarak görebilirsiniz. Bu yazımda sadece Image componentine ZoomIn/Zoom Out özelliğini nasıl kazandırabileceğimizden bahsedeceğim.

Aslında bu özelliğin bir property olarak verilmemesi büyük hata, tahminimce yeni gelecek güncellemelerle bir property ile bu özellik de eklenecektir. O gelene kadar aşağıdaki çözümü kullanabiliriz.

Aslında mantık basit, _*Keep It Simple*_ prensibine uygun. Kodun yaptığı şey şu;

* Kullanıcının büyütme-küçültme (Pinch) hareketini -meşhur parmak hareketi- yakalayıp Content içindeki elemanı o oranda yakınlaştır,
* Kullanıcının sürükleme (Pan) hareketini yakalayıp Content içinde gösterilen kısmın ekrandaki yerini belirle,
* Çift tıklama (Tap) hareketini yakalayıp Content içindeki değer daha önce büyütülmüşse orjinal boyuta döndür, orjinal boyuttaysa bir miktar yakınlaş.

__Xamarin forumlarından aldığım örnekler üzerine;__

*	Çift tıklama ile fotoğrafın orijinal boyutuna dönmesi özelliğini ekledim,
*	Parçalı sınıflara bağımlılığını kopardım,
*	Şekil şemailini düzelttim.

__Kullanmak için;__

*	ZoomImageBehavior.cs sınıfını projenizde istediğiniz bir yere kopyalayın,
*	Özelliği ekleyeceğiniz sayfanın Xaml koduna aşağıdaki şekilde image componentini ekleyin,

Ekranın bir Xaml ı yoksa Behavior u koddan da verebilirsiniz. O da şu şekilde;

    imgActivePage.Behaviors.Add(new ZoomImageBehavior{ IsScaleEnabled=true, IsTranslateEnabled=true })

<span class="color-red"> !!! Dikkat !!!</span>

Image component inin bir ContentView içinde olması gerekiyor. Behavior kodlarını incelediğinizde Parent nesnesi olarak bir ContentView aradığını göreceksiniz. Parent ı burda değiştirecekseniz behavior içinde de değiştirmeniz gerekir.

Faydalı olması ümidiyle, herkese kolay gelsin.


### Xaml Tarafı
<script src="https://gist.github.com/asozyurt/7d57dcb943644ac24a271af9a3de6231.js"></script>

### Kod Tarafı

<script src="https://gist.github.com/asozyurt/6b637b5c3425791814746a57d7608bd5.js"></script>

__Kaynak__

[https://developer.xamarin.com/guides/xamarin-forms/user-interface/gestures](https://developer.xamarin.com/guides/xamarin-forms/user-interface/gestures)



