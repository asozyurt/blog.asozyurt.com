# Blog Engine For .Net Core Developers

This is a file-based, easy to customize and fast Asp.Net Core blog engine. 

I added some new features over the original repo and changed appearance. After that used as my personal blog - [blog.asozyurt.com](http://blog.asozyurt.com) - previously on wordpress.

### New Features

* MarkDown format support by using [MarkDig](https://github.com/lunet-io/markdig)
* Featured Image property added
* Category property added to blog posts and search/list by category
* .NetCore upgrade to version 2.2 (will be updated to 3.* when it is ready)
* [Docker](https://www.docker.com/) support
* In-memory cache to increase performance
* CORS setup for api usage

#### What's Next

* Language Support
* Resource bundling
* Admin Panel 
	* Secure Logon
	* Blog Post Edit (_Prototype api is ready_)
	* Blog Settings Edit
* Docker-Hub Image

#### Considering

* Database usage - Not sure, actually I don't want it but at some point it would be necessary 
* Change where blog posts and settings are stored
* Cloud Hosting

#### Adding A New BlogPost
* Add your Md or Html file to __blogSource_ directory
* Edit _blogPosts.json_ and insert new post metadata like this:

```json
	{
      "published": true,
      "title": ".NET CORE 3 ÇIKIYOR - YENİLİKLER NELER",
      "description": "Teknoloji gelişiyor, teknoloji geliştirme araçları da gelişiyor ve bazen sadece haberlerini bile takip etmekte zorlanacağınız bir hıza ulaşıyor. '.Net Core' nispeten yeni bir teknoloji ve yeni yeni yaygınlaşıyor diyebiliriz.",
      "image": "http://blog.asozyurt.com/img/dotnetCore.png",
      "createdate": {
        "year": "2018",
        "month": "12",
        "day": "15",
        "display": " 15 Ara 2018"
      },
      "Author": "Ahmet Selçuk Özyurt",
      "slug": "dotnet-core-3-cikiyor-yenilikler-neler",
      "view": "dotnet-core-3-cikiyor-yenilikler-neler.md",
      "tags": [ ".NetCore", ".NetCore 3", "Development", "Teknoloji" ],
      "categories": [ "Teknoloji", ".NetCore" ]
    }
```


##### Here is a screenshot for mainpage

![](http://blog.asozyurt.com/img/blog_screenshot.jpg)]
