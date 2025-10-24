# Match 3 Oyunu - Candy Crush Tarzı

Unity için Candy Crush benzeri tam özellikli Match 3 oyunu implementasyonu.

## Özellikler

- 8x8 ızgara tahtası
- 6 farklı renkli taş (Kırmızı, Mavi, Yeşil, Sarı, Mor, Turuncu)
- Bitişik taşları takas etmek için kaydırma kontrolleri
- Satır/sütunda 3 veya daha fazla taş eşleşmesi tespiti
- Otomatik taş düşme ve yenileme
- Zincirleme eşleşmeler
- Skor sistemi

## Kurulum Talimatları

### 1. Match3 Sahnesini Oluşturun

1. Unity'de yeni bir sahne oluşturun: `File > New Scene`
2. `Assets/Scenes/` klasörüne `Match3Scene` olarak kaydedin

### 2. Gem Prefab'ı Oluşturun

1. Yeni bir 2D Sprite GameObject oluşturun: `GameObject > 2D Object > Sprite`
2. Adını "Gem" olarak değiştirin
3. `Gem.cs` script bileşenini ekleyin
4. `Sprite Renderer` bileşeni ekleyin (varsayılan olarak olmalı)
5. Sprite'ı bir daire veya kare sprite olarak ayarlayın (Unity'nin yerleşik sprite'ı veya kendiniz oluşturun)
6. `Circle Collider 2D` veya `Box Collider 2D` bileşeni ekleyin
7. "Gem" GameObject'ini `Assets/Scripts/Match3/` klasörüne sürükleyerek prefab oluşturun
8. Gem'i sahne hiyerarşisinden silin

### 3. Tahtayı Kurun

1. Boş bir GameObject oluşturun: `GameObject > Create Empty`
2. Adını "Board" olarak değiştirin
3. `Board.cs` script bileşenini ekleyin
4. Inspector'da, oluşturduğunuz Gem prefab'ını "Gem Prefab" alanına atayın
5. Width ve Height değerlerini 8 olarak ayarlayın (veya tercih ettiğiniz boyut)

### 4. MatchFinder Oluşturun

1. Boş bir GameObject oluşturun: `GameObject > Create Empty`
2. Adını "MatchFinder" olarak değiştirin
3. `MatchFinder.cs` script bileşenini ekleyin

### 5. UI'ı Kurun

1. Bir Canvas oluşturun: `GameObject > UI > Canvas`
2. Canvas Scaler'ı "Scale with Screen Size" olarak ayarlayın
3. Bir Text elementi oluşturun: `Canvas'a sağ tık > UI > Text`
4. Adını "ScoreText" olarak değiştirin
5. Ekranın üst kısmına yerleştirin
6. Metni "Score: 0" olarak ayarlayın
7. Yazı tipi boyutunu ve rengini istediğiniz gibi özelleştirin

### 6. ScoreManager Oluşturun

1. Boş bir GameObject oluşturun: `GameObject > Create Empty`
2. Adını "ScoreManager" olarak değiştirin
3. `ScoreManager.cs` script bileşenini ekleyin
4. Oluşturduğunuz ScoreText'i Inspector'daki "Score Text" alanına atayın

### 7. Kamerayı Ayarlayın

1. Main Camera'yı seçin
2. Camera'nın pozisyonunu 8x8 tahtayı ortalamak için yaklaşık (3.5, 3.5, -10) olarak ayarlayın
3. Camera'nın Projection'ını Orthographic olarak ayarlayın
4. Tahtanın güzel görünmesi için Size'ı 5 veya 6 olarak ayarlayın

### 8. Proje Ayarlarını Yapılandırın

1. `Edit > Project Settings > Physics 2D` yoluna gidin
2. Gravity'nin 0 olarak ayarlandığından emin olun (fizik yerçekimi istemiyoruz)

## Nasıl Oynanır

1. Bir taşa tıklayın ve herhangi bir yöne sürükleyin (yukarı, aşağı, sol, sağ)
2. Taş, o yöndeki bitişik taşla yer değiştirecek
3. Takas aynı renkte 3 veya daha fazla taş eşleşmesi oluşturursa, kaybolacaklar
4. Boş alanları doldurmak için yukarıdan yeni taşlar düşer
5. Zincirleme eşleşmeler otomatik olarak tespit edilir ve puanlanır
6. Eşleşen her taş için skor 10 puan artar

## Script Açıklamaları

- **Gem.cs**: Bireysel taş davranışını kontrol eder, girişi işler (kaydırma algılama) ve taş hareketini yönetir
- **Board.cs**: Oyun tahtasını, taş ızgarasını, takas mantığını, eşleşme işlemesini ve taş düşme mekaniklerini yönetir
- **MatchFinder.cs**: Tahtadaki tüm eşleşmeleri tespit eder (yatay veya dikey olarak bir sırada 3+ taş)
- **ScoreManager.cs**: Oyuncunun skorunu yönetir ve UI'ı günceller

## Özelleştirme

### Tahta Boyutunu Değiştirme
Board bileşeninde `Width` ve `Height` değerlerini ayarlayın.

### Taş Türü Sayısını Değiştirme
`Gem.cs` içindeki `GemType` enum'unu düzenleyin ve `Board.cs` SetUp() metodundaki rastgele aralığı güncelleyin.

### Renkleri Değiştirme
`Gem.cs` içindeki `SetColor()` metodunu farklı renkler veya özel sprite'lar kullanacak şekilde değiştirin.

### Puanlamayı Değiştirme
`Board.cs` DestroyMatches() metodundaki puanlama mantığını değiştirin.

## Geliştirme İpuçları

1. **Sprite Ekleyin**: Renkli daireleri özel şeker/taş sprite'ları ile değiştirin
2. **Parçacık Efektleri Ekleyin**: Taşlar yok edildiğinde parçacık sistemleri ekleyin
3. **Ses Efektleri Ekleyin**: Takaslar, eşleşmeler ve zincirlemeler için ses ekleyin
4. **Animasyonlar Ekleyin**: Pop/crush animasyonları eklemek için Unity'nin Animator'ını kullanın
5. **Özel Taşlar Ekleyin**: 4+ eşleşmeler için özel taşlar (bombalar, satır temizleyiciler vb.) uygulayın
6. **Seviyeler Ekleyin**: Hedefler ve hamle limitleri ile bir seviye sistemi oluşturun
7. **Güçlendirmeler Ekleyin**: Oyuncunun etkinleştirebileceği özel yetenekler uygulayın

## Bilinen Sınırlamalar

- Şu anda basit renkli sprite'lar kullanıyor (özel tasarım yok)
- Basit puanlama sistemi
- Seviye ilerlemesi veya hedefler yok
- Özel taş türleri yok
- Animasyonlar veya parçacık efektleri yok
- Ses efektleri yok

Oyununuzun tadını çıkarın!
