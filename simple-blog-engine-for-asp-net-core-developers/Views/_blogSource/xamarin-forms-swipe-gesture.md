Xamarin Formstaki temel sorunlardan birisi de Gesture (Kullanıcı Etkileşimleri) ların yetersiz olması. Pan (Kaydırma), Pinch (Çift Parmak) ve Tap (Tıklama) hareketleri için bir yapı var evet ama çoğu uygulamada kullandığımız Swipe (Kaydırma) için bir yapı yok.

Universalist Dergi uygulamasını ilk yapıp arkadaşlardan test etmelerini istediğimde hemen hepsinin ilk söylediği şey sayfalar arası geçişte butonları kullanmak zorunda olmanın can sıkıcı olduğuydu. Haklılar !</p>

İnternette uzunca bir araştırma yapmama rağmen bu ihtiyacı karşılayacak bir çözümle karşılaşmadım. Tam vazgeçmiştim, can sıkıcı da olsa butonlarla Store a çıkacaktım ki aklıma insanlık için tırt ama benim için dahiyane bir çözüm geldi. Geçtim bilgisayarın başına, bir iki ufak deneme yaptım. Çalıştı !

Bulduğum çözüm, halihazırda yakaladığım Pan hareketini hız ve doğrultusunu hesaplayarak gerçekte istenenin Swipe olup olmadığını anlamak. Bunu da behavior a küçük bir eklenti yaparak hallettim. Mantık;

* Pan hareketinin başlangıç ve bitiş zamanlarını tut,
* Pan hareketinde X ve Y koordinatlarının değişimlerini bul,
* X koordinatı 35 birimden fazla değiştiyse ve bu değişim 1500000 tick süresinden daha az bir sürede gerçekleştiyse Swipe yapılmak istenmiştir. Değişimin yönüne göre sağ veya sol olmasına karar ver.

Ben bu projede kullanmadım ama aynı mantıkla Y koordinatındaki değişimi de yakalayıp aşağı/yukarı kaydırma hareketleri de yakalanabilir. __OnPanUpdated__ metodunu inceleyebilirsiniz.

<script src="https://gist.github.com/asozyurt/6b637b5c3425791814746a57d7608bd5.js"></script>

Projede yalnızca Image bileşeni için denedim, diğer bileşenlere de uygulanabileceğini düşünüyorum, çünkü zaten Image için olan örnekte de tüm event ler ContentView a tanımlanıyor. Yeni bir proje açıp bu örnekleri de eklemeyi düşünüyorum. Tamamladığımda buradan paylaşırım.

Projenin koduna <a href="https://github.com/asozyurt/universalistDergiRc" target="_blank">şuradan</a>, yalnızca bu kısım yeterli derseniz de ZoomImageBehavior koduna <a href="https://github.com/asozyurt/universalistDergiRc/blob/master/UniversalistDergiRC/Core/ZoomImageBehavior.cs" target="_blank">şuradan</a> ulaşabilirsiniz.

Herkese kolay gelsin.