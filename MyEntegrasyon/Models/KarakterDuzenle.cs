namespace MyEntegrasyon.Models
{
    public class KarakterDuzenle
    {
        public string Guncelle(string karakter)
        {


           
            karakter = karakter.ToUpper(); // Dizideki tüm küçük harfleri büyük harfe çevirir

            //A B C Ç D E F G Ğ H İ I J K L M N O Ö P R S Ş T U Ü V Y Z
            karakter = karakter.Replace("Ç", "C");
            karakter = karakter.Replace("Ğ", "G");
            karakter = karakter.Replace("İ", "I");
            karakter = karakter.Replace("Ö", "O");
            karakter = karakter.Replace("Ş", "S");
            karakter = karakter.Replace("Ü", "U");

            return karakter;
        }



    }
}
