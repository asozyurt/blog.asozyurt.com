var DEFAULT_LANGUAGE = 'tr';
var LANGUAGE_ENGLISH = 'en';
var LANGUAGE_TURKISH = 'tr';
var LANGUAGE_CACHE_CODE = 'SITE_LANG';

$(document).ready(function () {
    let langCode = localStorage.getItem(LANGUAGE_CACHE_CODE);
    if (langCode && langCode !== DEFAULT_LANGUAGE) {
        changeLang(langCode);
    }
    else
        langCode = DEFAULT_LANGUAGE;
    if (langCode) {
        localStorage.setItem(LANGUAGE_CACHE_CODE, langCode);
    }
});

function Language(lang) {
    var __construct = function () {
        if (eval('typeof ' + lang) == 'undefined') {
            lang = DEFAULT_LANGUAGE;
        }
        return;
    }()

    this.getStr = function (str, defaultStr) {
        var retStr = eval('eval(lang).' + str);
        if (typeof retStr != 'undefined') {
            return retStr;
            //} else {
            //    if (typeof defaultStr != 'undefined') {
            //        return defaultStr;
            //    } else {
            //        return eval(DEFAULT_LANGUAGE + '.' + str);
            //    }
        }
        else return;
    }
}

function changeLang(destinationLang) {
    let languageCode = destinationLang;
    if (!languageCode) {
        languageCode = localStorage.getItem(LANGUAGE_CACHE_CODE)
        if (!languageCode) return;
        languageCode = languageCode === LANGUAGE_TURKISH ? LANGUAGE_ENGLISH : LANGUAGE_TURKISH;
    }
    else if (languageCode === DEFAULT_LANGUAGE) {
        return;
    }
    if (!languageCode)
        return;
    let translator = new Language(languageCode);
    document.title = translator.getStr("SiteTitle");

    let allIds = document.querySelectorAll('*[id]');

    $.each(allIds, function (index, value) {
        let translatedText = translator.getStr(value.id);
        if (translatedText) {
            $('#' + value.id).text(translatedText);
        }
    });

    if (languageCode) {
        localStorage.setItem(LANGUAGE_CACHE_CODE, languageCode);
    }
    $('meta[property="description"]').attr('content', translator.getStr("SiteDescription"));
}

var en = {
    SiteTitle: "Ahmet Selcuk Ozyurt - Personal Website",
    SiteDescription: "Passionate developer for end2end projects, mobile, web and desktop applications.",

    menuHome: "Home",
    menuAbout: "About",
    menuResume: "Resume",
    shortBio: "Developer / Software Architect",
    menuPortfolio: "Portfolio",
    menuBlog: "Blog",
    menuContact: "Contact",
    error: "An error occured when sending your message",
    success: "",

    switchLanguage: "   Türkçe Sayfaya Git",
    footerCopyRight: "© 2018 All Rights Reserved    ",
    siteSubHeader: "Developer, father and one step further!"
};

var tr = {
    SiteTitle: "Ahmet Selçuk Özyurt - Kişisel Web Sitesi",
    SiteDescription: "Adana'nın Boyozu, İzmir'in Şırdanı. Bilgisayarlı bir mühendis, çocuklu bir baba. Uçtan uca yazılım üreticisi, kod yazmaya tutkun bir yazılımcı.",

    menuHome: "Anasayfa",
    menuAbout: "Hakkımda",
    menuResume: "Özgeçmiş",
    shortBio: "Bilg. Müh. / Yazılım Tasarım",
    menuPortfolio: "Çalışmalar",
    menuBlog: "Blog",
    menuContact: "İletişim",

    switchLanguage: "   Switch To English",
    footerCopyRight: "© 2018 Tüm Hakkı Bulut    ",
    siteSubHeader: "Valla bende çalışıyor!"
};