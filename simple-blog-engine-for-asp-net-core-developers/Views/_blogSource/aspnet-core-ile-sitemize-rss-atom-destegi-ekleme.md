.Net Core bu aralar favorim, iş yerimde de, şahsi projelerimde de bu teknoloji üzerine gidiyorum. Ve daha önce de belirttiğim gibi bu blog u .Net Core ile sevgili _Gareth Elms_ abimizin yazdığı [orijinal repository](https://github.com/GarethElms/simple-blog-engine-for-asp-net-core) üzerine irili ufaklı bir çok özellik ekleyerek yazıyorum. 

Adım adım gelişmesini görmek, hem öğrenmek hem de öğrendiklerimden bu şekilde içerik üretmek çok hoşuma gidiyor.

Bu konuda düzenleme işine girişmeden evvel Rss in artık miadını doldurmuş, ununu elemiş, eleğini asmış olduğunu düşünüyordum. Sonra baktım ki, 70lik delikanlı, hala gayet yaygın, kullanım alanları halen aktif, üzerine yeni yeni ürünler inşa edilmiş.

<p class="text-center"><img class="img-fluid" title="Şu an gözümde Rss" src="http://blog.asozyurt.com/img/oldMan.jpg" /></p>

Medyatik bir içerik üreticisi değilseniz -ki değiliz çoğumuz- ürettiklerinizi hedef kitlenize sizin ulaştırmanız gerekiyor. Diyelim ki bir yazınız ile belli bir kitleye ulaştınız, bu kişilerle sürekli bir bağ kurabilmek için güncel içeriklerden onları haberdar etmeniz gerekiyor.

Blog a yeni bir içerik eklendiğinde bunu bir şekilde takipçilere bildirmemiz gerekiyor. Bunun da _3_ yolu var;

* Sitenize girme ihtimali olan herkese Sms atmak (arayabilirsiniz de, daha samimi olur),
* E posta üyelik tutup, kaydolanlara e-posta göndermek
* Siteye __Feed__ (_Rss - Atom)_ eklemek,

Birinci şıkkı eliyorum, çünkü telefonunuzun şarjını bitirebilir, beklediğiniz iş görüşmesini riske sokabilir. Yoksa favorim bu :)

İkinci şık için öncelikle ziyaretçilerinizi size e-posta adreslerini vermeleri konusunda ikna etmeniz gerekiyor, o yüzden onu da eliyorum.

Üçüncü seçenek ile devam edebiliriz. Öncelikle her zamanki gibi kısa kısa tanımlamalarımızı yapalım.


## Rss Nedir?
Kullanıcı ve bilgisayarların online bir kaynaktaki güncellemelere hızlıca erişmelerini sağlayan standart xml tabanlı içerik paylaşım ve iletim metodudur. 1990 larda _Rich Site Summary_ (Zengin Site Özeti) olarak, son zamanlarda da _Really Simple Syndication_ (Aşşırı Kolay Toplu Yayın) olarak kullanılmaya başlanmış. İlk karşıma çıktığında yazarın eğlencesine böyle bir isim verdiğini düşünmüştüm ama gerçekten de bu şekilde kullanımda.


## Atom Nedir?
Rss in çok benzeri, Rss e alternatif olarak ortaya çıkmış yine xml tabanlı bir içerik yayın formatı. Yazının sonunda da göreceğiz ki Rss den farkı birkaç ilave tag ile bazı tag lerin isimlerinin farklı olması. Rss deki bazı kısıtlamaları kaldırmak için yapılmış.

Bu iki kardeş için güzel bir karşılaştırmayı [şu adreste](https://www.saksoft.com/rss-vs-atom/) bulabilirsiniz.

## Rss - Atom Ne İşe Yarar?

Yukarıda sözlük tanımlarını versek de kısaca; web site içeriklerinin standart bir formatla yayınlanan özetleridir (_feed_) diyebiliriz. Bu __feed__ ler sayesinde takip ettiğimiz birden fazla kaynaktaki güncellemelere kolay bir şekilde ulaşabiliriz. Hatta bazı filtreler uygulayıp tamamen kendimize özgü bir online gazete oluşturabiliriz. Bir başka kullanım alanı olarak da, başkalarının yayınladığı _feed_ leri kullanarak site içeriğimizi zenginleştirebiliriz.

#### Bu kadar gevezelik yeterli sanırım, artık koda geçelim.

Özelliği Asp.Net Core MVC yapısındaki bir siteye eklemeyi anlatacağım, küçük uyarlamalarla başka yapılardan da kullanılabilir. Gerekli adımlar şöyle;
-	Feed olarak sunacağımız veriyi hazırlamak,
-	Controller üzerinde veriyi formatlayarak döndürmek
-	Sitede açacağımız adres için routing ayarlarını yapmak


### Veriyi Hazırlama

Kodumuzun herhangi bir yerinde feed verisini dönen metodu hazırlıyoruz. Kategoriye göre filtreleme, son _n_ tane kaydı dönme gibi özelleştirmeleri bu metoda yetenekler ekleyerek yapabiliriz. Ben ayrım yapmaksızın tüm yazıları dönen bir metot hazırladım.

Görüldüğü gibi oldukça sade, _SyndicationItem_ nesnesi yaratıp blog postunun özelliklerini set ediyoruz.	

	private IList<SyndicationItem> GetFeed()
	{
		IList<SyndicationItem> result = new List<SyndicationItem>();

		foreach (var post in _blogPostsSettings.Blogs.Where(x => x.Published))
		{
			var item = new SyndicationItem()
			{
				Title = post.Title,
				Description = post.Description,
				Id = post.Slug,
				Published = post.CreateDate.Date,
				LastUpdated = post.CreateDate.Date
			};

			post.Categories.ForEach(x => item.AddCategory(new SyndicationCategory(x)));
			item.AddLink(new SyndicationLink(new System.Uri(_siteSettings.SiteURL + Path.DirectorySeparatorChar + post.UrlTail)));
			item.AddContributor(new SyndicationPerson(post.Author, _siteSettings.Email));
			result.Add(item);
		}
		return result;
	}

### Controller Metotları

	public ContentResult Rss()
	{
		return FeedResult.Rss(_siteSettings, _blogPostsSettings);
	}
	public ContentResult Atom()
	{
		return FeedResult.Atom(_siteSettings, _blogPostsSettings);
	}


### Route eklemeleri
Feed ler sitede genel olarak /feed /rss gibi path lere konuluyor. Bizde /rss ve /atom altına koyacağız. Bunun için gerekli mapping leri Startup sınıfımıza ekliyoruz.

	app.UseMvc(routes =>
	{
		routes.MapRoute(
				name: "rss",
				template: "/rss",
				defaults: new { controller = "Home", action = "Rss" });
		routes.MapRoute(
			   name: "atom",
			   template: "/atom",
			   defaults: new { controller = "Home", action = "Atom" });

## BONUS - ESKİ SİSTEMLERE UYUM

Kodlamayı bitirip de bazı Rss validator siteleri üzerinden rss ve atom sayfalarımı test etmek istediğimde kötü bir sürprizle karşılaştım. Hemen hemen tüm validator lar rss in beklenen formatta olmadığını söylüyordu. Bu sorunu düzeltmek için kullandığım yöntemi çok beğenmediğim için blog yazısını yazmam aylar aldı.
Hızlı çözüm için FeedResult metodunda şu değişikliği yaptım

	Content = sw.ToString().Replace("utf-16", "utf-8");
	ContentType = "application/rss+xml; charset=utf-8";

Dediğim gibi, gurur duyduğum bir fix değil ama en azından sorunu ortadan kaldırıyorç
Daha sonra daha beğendiğim bir değişiklik yaparsam burayı da güncellerim, lakin şimdilik bu kadarıyla idare edeceğiz.

