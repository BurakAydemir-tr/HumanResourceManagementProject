# HumanResourceManagementProject
Human Resource Management System Project

Bu projede bir insan kaynakları yönetim sisteminin ana yapısını oluşturdum. 
.Net core 6.0 framework kullanarak bir web api projesi oluşturdum. Projede katmanlı Mimari yapısını kullandım. 
SOLID prensiblerine uygun bir proje oluşturmaya çalıştım.

### Technologies
- Restful Web Api Vers. .Net Core 6.0
- Multi-Layer Architecture
- Interceptor
- Validation Aspect(Fluent Validation)
- Authorization
- Authentication
- Json Web Token Managment
- SOLID

### List of things done
#### Req 1 : İş Arayanlar sisteme kayıt olabiliyor.
Kabul Kriterleri:
- Kayıt sırasında kullanıcıdan ad, soyad, tcno, doğum yılı, e-Posta, şifre, şifre tekrarı bilgileri istenir.
- Tüm alanlar zorunludur. Kullanıcı bilgilendirilir.
- Daha önce kayıtlı bir e-posta veya tcno var ise kayıt gerçekleşmez. Kullanıcı bilgilendirilir.

#### Req 2 : İş verenler sisteme kayıt olabiliyor
Kabul Kriterleri:
-	Kayıt sırasında kullanıcıdan şirket adı, web sitesi, web sitesi ile aynı domaine sahip e-posta, telefon, şifre, şifre tekrarı bilgileri istenir. Burada amaç sisteme şirket olmayanların katılmasını engellemektir.
-	Tüm alanlar zorunludur. Kullanıcı bilgilendirilir.
-	Daha önce kayıtlı bir e-posta var ise kayıt gerçekleşmez. Kullanıcı bilgilendirilir.

#### Req 3 : Sisteme genel iş pozisyonu isimleri ekleniyor.. Örneğin Software Developer, Software Architect.
Kabul Kriterleri:
-	Bu pozisyonlar tekrar edemez. Kullanıcı uyarılır.
  
#### Req 4 : İş verenler listelenebiliyor. (Sadece tüm liste)
#### Req 5 : İş arayanlar listelenebiliyor. (Sadece tüm liste)
#### Req 6 : İş pozisyonları listelenebiliyor. (Sadece tüm liste)
#### Req 7 : İş verenler sisteme iş ilanı ekleyebiliyor.
-	İş ilanı formunda;
-	Seçilebilir listeden (dropdown) genel iş pozisyonu seçilebilmelidir.(Örneğin Java Developer)(Zorunlu)
-	İş tanımı girişi yapılabilmelidir. (Örneğin; firmamız için JAVA, C# vb. dillere hakim....)(Zorunlu)
-	Maaş skalası için min-max girişi yapılabilmelidir. (Opsiyonel)
-	Açık pozisyon adedi girişi yapılabilmelidir. (Zorunlu)
-	Son başvuru tarihi girişi yapılabilmelidir.
#### Req 8 : Sistemdeki tüm aktif iş ilanları listelenebiliyor.
#### Req 9 : Sistemdeki tüm aktif iş ilanları tarihe göre listelenebiliyor.
#### Req 10 : Sistemde bir firmaya ait tüm aktif iş ilanları listelenebiliyor.
#### Req 11 : İş verenler sistemdeki bir ilanı kapatabiliyor. (Pasif ilan)
#### Req 12: Adaylar sisteme CV girişi yapabiliyor.
-	Adaylar okudukları okulları sisteme ekleyebilmelidir. (Okul adı, bölüm)
-	Bu okullarda hangi yıllarda okuduklarını sisteme girebilmelidir.
-	Eğer mezun değilse mezuniyet yılı boş geçilebilmelidir.
-	Adayların okudukları okullar mezuniyet yılına göre tersten sıralanabilmelidir. Mezun olunmamışsa yine bu okul en üstte ve "devam ediyor" olarak görüntülenmelidir.
-	Adaylar iş tecübelerini girebilmelidir. (İş yeri adı, pozisyon)
-	Bu tecrübelerini hangi yıllarda yaptıklarını sisteme girebilmelidir.
-	Eğer hala çalışıyorsa işten ayrılma yılı boş geçilebilmelidir.
-	Adayların tecrübeleri yıla göre tersten sıralanabilmelidir. Hala çalışıyorsa yine bu tecrübesi en üstte ve "devam ediyor" olarak görüntülenmelidir.
-	Adaylar sisteme fotoğraf girebilmelidir.
-	Adaylar bildikleri programlama dillerini veya teknolojilerini sisteme girebilmelidir. (Programlama/Teknoloji adı) Örneğin; React
-	Adaylar sisteme ön yazı ekleyebilmelidir. 
#### Req 13 : Bir adaya ait tüm CV bilgisi görüntülenebiliyor.
#### Req 14 : İşveren ve iş arayanlar olmak üzere iki tür kullanıcı olduğu için user tablosu üzerinden Table per Type kalıtımsal davranışı kullanıldı.
#### Req 15 : Şifre güncelleme için email doğrulaması eklendi.
#### Req 16 : Rol ekleme eklendi.
#### Req 17 :Her bir Endpoint’in bilgileri veri tabanına kaydı yapılabiliyor ve bu endpointler roller ile ilişkilendirilebiliyor.
#### Req 18: Global exception yapısı eklendi.
#### Req: 19: Pagination yapısı oluşturuldu.
#### Req: 20: SeriLog yapısı eklendi. Hem dosyaya hemde veritabanına log kayıtları atılıyor.
