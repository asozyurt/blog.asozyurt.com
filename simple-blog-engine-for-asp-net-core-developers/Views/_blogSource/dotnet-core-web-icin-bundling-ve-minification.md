# .Net Core Web Uygulamalarında Bundling ve Minification

İrili ufaklı bütün projelerimizde birden fazla css ve js dosyaları kullanıyoruz. Kullandığımız temalardan, eklediğimiz üçüncü parti bileşenlerden ve kendi yazdığımız özelliklerden gelen bazen sayısı 20leri bile geçen dosyalar oluyor projemizde.

_İyi de ne var bunda, yönetmesi daha kolay değil mi böyle?_  

Evet yönetmesi daha kolay, ancak iş kodu yazmakla bitmiyor, canlı ortamda performanslı çalışmasını da istiyoruz. Sitemize girenlerin bize maksimum 3 saniye mühlet verdiğini unutmayalım.

[Kissmetrics](https://www.nngroup.com/articles/how-long-do-users-stay-on-web-pages/) verilerine göre, ziyaretçilerin %47 si sayfanın 2 saniyeden daha kısa sürede açılmasını bekliyor, ve %40 ı da yüklenme süresi 3 saniyeyi aşarsa sayfayı kapatıyor.

Her farklı dosya demek, web sunucusuna yapılacak olan bir istek demek. Dolayısı ile 20 dosya için _git-gel, git-gel..._ gereksiz bir sürü yük. Hem istemci tarafa hem de sunucu tarafına eziyet. Pazar alışverişi yaparken domatesi alıp eve getirmek, geri gidip portakalı alıp getirmek, tekrar gidip marul alıp getirmek ne kadar meşakkatli ise bu sunucu-istemci arası git-gel ler de o kadar meşakkatli.

### Bundling Nedir?
Bundling birden çok dosyanın tek dosyada birleştirilmesi işlemidir. Bir web uygulaması veya web sayfasının görüntülenebilmesi için sunucuya yapılan istek sayısını azaltmak için kullanılır. CSS, JavaScript gibi sabit kaynaklarınız(static resource) için ayrı ayrı bundle dosyaları oluşturabilirsiniz. Amaç ilk yüklenme performansını artırmaktır, ikinci ve sonraki isteklerde browser cache devreye girdiğinden bundle olup olmaması çok bir şey farkettirmez.

### Minification
Minification dosyadaki yorum, boşluk gibi gereksiz tüm kısımların silinmesi işlemidir Sonuç olarak orijinal dosyaya göre onlarca kat daha küçük bir dosya çıktısı oluşur. Çoğu minification işleminde değişken isimleri de tek harfle değiştirilerek verim artırılır.


## Çözüme Geçecek Olursak

Her konuda olduğu gibi bu konuda da bir çok farklı yöntem var. Ben en rahat şekilde projeme monte ettiğim aşağıdaki yöntemden bahsedeceğim. 

* Visual Studio eklentilerden __Bundler & Minifier Extension__ ı kuruyoruz.

![Visual Studio Marketplace](http://blog.asozyurt.com/img/bamExtension.jpg)

* Projeyi yaratırken eğer oluşturulmamışsa kök dizinde __bundleconfig.json__ dosyasını oluşturuyoruz. 

Bu blog un bundle config aşağıdaki gibi. 

_outputFileName_ ile birleştirilen ve küçültülen dosyanın nereye çıkarılacağını belirtiyoruz.

_inputFiles_ kısmına da hangi dosyaları dahil edeceksek onları yazıyoruz. 

Aşağıdan da görebileceğiniz üzere * ile o dizindeki tüm css dosyalarını dahil et diyebiliyoruz. Dahil olmasını istemediğimiz dosyalar için ünlem (!) işaretini başa koyarak yazıyoruz. 
```javascript
	[
	  {
		"outputFileName": "wwwroot/css/bundle.min.css",
		"inputFiles": [
		  "wwwroot/css/all/*.css", // globbing patterns are supported
		  "!wwwroot/css/syntax.css"
		],
		"minify": {
		  "enabled": true,
		  "commentMode": "none"
		}
	  },
	  {
		"outputFileName": "wwwroot/js/bundle.min.js",
		"inputFiles": [
		  "wwwroot/js/all/*.js",
		  "!wwwroot/js/all/highlight.pack.js", // ünlem ile belirtilen dosyalar işleme alınmaz
		]
	  }
	]
```

#### Eklenti indi, config hazır, artık dosyaları birleştirebiliriz.

Eklenti kurulumunda sorun olmadıysa projenizde bundleconfig.json dosyasına sağ tıkladığınızda Bundler&Minifier menüsünü görmeniz gerekir. Dosyaları oluşturmak için bu menünün altından __Update Bundles__ seçeneğini kullanabilirsiniz.

BundleConfig dosyası düzgün ayarlandıktan sonra bundan sonrasını Visual Studio ya bırakabilir veya kendiniz devam edebilirsiniz. Her buildde dosyanın yeniden oluşturulmasını isterseniz aşağıdaki seçeneği işaretlemeniz yeterli.

![Bundle Options](http://blog.asozyurt.com/img/bundleOptions.jpg)

#### Artık bundle ımız hazır, şimdi projemizde kullanalım

Projemizde css leri eklediğimiz kısma geliyoruz. Eski hali şöyle idi;

```html
<link rel="stylesheet" href="~/css/layout.css" /
<link rel="stylesheet" href="~/css/font-awesome.css" /

<link href="~/css/magnific-popup.css" type="text/css" rel="stylesheet" media="all" /
<link href="~/css/font.css" rel="stylesheet" type="text/css" media="all"
<link href="~/css/base.css" rel="stylesheet" type="text/css" media="all" /
<link href="~/css/main.css" rel="stylesheet" type="text/css" media="all" /
<link href="~/css/owl-carousel/owl.theme.css" rel="stylesheet" media="all"
```

Bundle sonrası şu şekilde olmalı; 

```html
<link rel="stylesheet" href="~/css/bundle.min.css" /
```

#### Hepsi Bu Kadar

Artık sunucuyu üzmeyen, istemciyi yormayan bir uygulamanız var. Güle güle kullanın.

Selçuk sen uzatmışsın işi, daha kolayı vardı bunun diyen varsa öğrenmeyi çok isterim ayrıca.
Herkese iyi tatiller. 