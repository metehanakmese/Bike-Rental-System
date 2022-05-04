using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;   

namespace Proje_3
{
    class Durak
    {
        public string durakAdi;
        public int bosPark;
        public int normalBisiklet;
        public int tandemBisiklet;
        public Durak(string[] kelimeler)      // Parametre olarak string dizisi alan ve bunları ayrıştırıp 
        {                                     // durağın özelliklerine atayan constructor
            this.durakAdi = kelimeler[0];
            this.bosPark = Convert.ToInt32(kelimeler[1]);
            this.tandemBisiklet = Convert.ToInt32(kelimeler[2]);
            this.normalBisiklet = Convert.ToInt32(kelimeler[3]);          
        }       
    }
    class Musteri
    {
        public int musteriID;
        public string kiralamaSaati;        
        Random random = new Random();
        public Musteri(int musteriID)       // Parametre olarak ID alan Müşteri sınıfının Constructor ı
        {     
            this.musteriID = musteriID;     
        }
        public void setKiralamaSaati()      // Random kiralama saati oluşturma
        {
            this.kiralamaSaati =  random.Next(0, 24) + ":" + random.Next(0, 60);        
        }     
    }
    class Dugum
    {
        public Durak durak;
        public Dugum sol;
        public Dugum sağ;
        public List<Musteri> gelenMusteri;
        public Dugum()
        {
            gelenMusteri = new List<Musteri>();     // Durağa gelen müşterileri tutmak için Müşteri tipinde Generic List
        }
        public void Goster()
        {   // Durak bilgilerini ekrana yazdırma
            Console.WriteLine("\nDurak Adı : "+durak.durakAdi + "\tBoş Park Sayısı : " + durak.bosPark + "\tTandem Bisiklet Sayısı : " + durak.tandemBisiklet + "\tNormal Bisiklet Sayısı : " + durak.normalBisiklet);
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Kiralama Yapan Müşteri Sayısı : " + gelenMusteri.Count());
            // Duraktaki Müşterileri ekrana yazdırma
            foreach (Musteri item in gelenMusteri)          
                Console.WriteLine("Müşteri ID : "+item.musteriID+ "\t\tKiralama Saati : "+  item.kiralamaSaati);           
            Console.WriteLine();
        }
    }
    class DurakAgaci
    {
        public Dugum kok;
        public List<Musteri> genericMusteriListesi = new List<Musteri>();
        Random random = new Random();
        public DurakAgaci()
        {
            kok = null;
            for (int i = 1; i < 21; i++)
            {
                Musteri musteri = new Musteri(i);
                genericMusteriListesi.Add(musteri);
            }
        }
        public Dugum getKok()
        {
            return this.kok;
        }
        
        public void Ekle(Durak durak)   // Ağaca durak ekleme metodu
        {          
            int index;
            int musteriSayisi = random.Next(1, 10);    // Durağa ekleyeceğimiz random sayıda müşteri sayısını hesaplama
            Dugum yeniDugum = new Dugum();
            yeniDugum.durak = durak;

            if (kok == null)
            {
                kok = yeniDugum;
                for (int i = 0; i < musteriSayisi; i++)
                {
                    index = random.Next(0, 20);                   
                    if (!kok.gelenMusteri.Contains(genericMusteriListesi[index]))  // Ekleyeceğimiz müşterinin listede olup olmadığını kontrol eder
                    {
                        kok.gelenMusteri.Add(genericMusteriListesi[index]);
                        genericMusteriListesi[index].setKiralamaSaati();     // Eklenen müşteriye random kiralama saati oluşturur
                        if (kok.durak.normalBisiklet > 0)
                            kok.durak.normalBisiklet--;         // Durakta kalan bisiklet sayılarını  ve boş parkı günceller 

                        else if (kok.durak.tandemBisiklet > 0)
                            kok.durak.tandemBisiklet--;                     
                        durak.bosPark++;
                    }
                }
            }
            else
            {
                Dugum mevcut = kok;
                Dugum ebeveyn;
                while (true)
                {
                    ebeveyn = mevcut;
                    if (durak.durakAdi.CompareTo(mevcut.durak.durakAdi) == -1)  // Durak adlarını alfabetik olarak karşılaştır 
                    {                                                           // Eğer küçükse durağı sol çocuğa ekler
                        mevcut = mevcut.sol;
                        if (mevcut == null)
                        {
                            ebeveyn.sol = yeniDugum;        // Yeni sol çocuk düğümü oluşturur
                            for (int i = 0; i < musteriSayisi; i++)
                            {
                                index = random.Next(0, 20); //1-20 arasında random ID oluşturma
                                if (!ebeveyn.sol.gelenMusteri.Contains(genericMusteriListesi[index]))   // Ekleyeceğimiz müşterinin listede olup olmadığını kontrol eder
                                {
                                    ebeveyn.sol.gelenMusteri.Add(genericMusteriListesi[index]);
                                    genericMusteriListesi[index].setKiralamaSaati();        // Eklenen müşteriye random kiralama saati oluşturur
                                    if (ebeveyn.sol.durak.normalBisiklet > 0)
                                        ebeveyn.sol.durak.normalBisiklet--;                 // Durakta kalan bisiklet sayılarını  ve boş parkı günceller 

                                    else if(ebeveyn.sol.durak.tandemBisiklet > 0)
                                        ebeveyn.sol.durak.tandemBisiklet--;                                   
                                    durak.bosPark++;                              
                                }
                            }
                            return;
                        }
                    }
                    else
                    {
                        mevcut = mevcut.sağ;
                        if (mevcut == null)
                        {
                            ebeveyn.sağ = yeniDugum;
                            for (int i = 0; i < musteriSayisi; i++)
                            {
                                index = random.Next(0, 20);
                                if (!ebeveyn.sağ.gelenMusteri.Contains(genericMusteriListesi[index]))   // Ekleyeceğimiz müşterinin listede olup olmadığını kontrol eder
                                {                                   
                                    ebeveyn.sağ.gelenMusteri.Add(genericMusteriListesi[index]);
                                    genericMusteriListesi[index].setKiralamaSaati();        // Eklenen müşteriye random kiralama saati oluşturur                                 
                                    if (ebeveyn.sağ.durak.normalBisiklet > 0)
                                        ebeveyn.sağ.durak.normalBisiklet--;                 // Durakta kalan bisiklet sayılarını ve boş parkı günceller                 
                                    else if (ebeveyn.sağ.durak.tandemBisiklet > 0)
                                        ebeveyn.sağ.durak.tandemBisiklet--;                                                              
                                    durak.bosPark++;
                                }
                            }
                            return;
                        }                   
                    }
                }
            }
        }
        public void InOrder(Dugum kok)    // In Order olarak sıralama metodu      
        {
            if (!(kok == null))
            {
                InOrder(kok.sol);
                kok.Goster();
                InOrder(kok.sağ);
            }
        }
        public int MaxDepth()
        {
            return MaxDepth(kok);
        }
        public int MaxDepth(Dugum kok)      // Ağacın derinliğini hesaplayan metot
        {                               
            if (kok == null)
                return -1;          
            else
            {
                int leftDepth = MaxDepth(kok.sol);
                int rightDepth = MaxDepth(kok.sağ);              
                return 1 + Math.Max(leftDepth, rightDepth);
            }
        }
        public void Ara(Dugum kok, int arananID)    // Aranan ID ye göre bütün ağacı dolaşan recursive fonksiyon 
        {
            if (!(kok == null))
            {
                Ara(kok.sol, arananID);                
                foreach (Musteri item in kok.gelenMusteri)
                {   // Aranan ID yi bulduğunda Müşterinin Kiralama yaptığı durak ve saati ekrana yazdırma
                    if (arananID == item.musteriID) 
                        Console.WriteLine("Kiralama yaptığı durak : " + kok.durak.durakAdi + "\t\tKiralama yaptığı saat : " + item.kiralamaSaati);
                }
                Ara(kok.sağ, arananID);
            }
        }
        public void Bul(int arananID)
        {
            Dugum mevcut = kok;
            Ara(kok, arananID);
        }
        public void kiralamaYap(string istasyonAdi, int ID)
        {
            Random random = new Random();
            string randomKiralamaSaati = random.Next(0, 24) + ":" + random.Next(0, 60); //Kiralama yapacak müşteriye random saat oluşturma
            Musteri yeniMusteri = new Musteri(ID);
            yeniMusteri.kiralamaSaati = randomKiralamaSaati;
            Dugum mevcut = kok;
            while (mevcut.durak.durakAdi != istasyonAdi)    // While döngüsüyle Kiralama yapılacak durağı bulana kadar ağacı gezme
            {
                if (istasyonAdi.CompareTo(mevcut.durak.durakAdi) == -1)
                    mevcut = mevcut.sol;
                else
                    mevcut = mevcut.sağ;               
            }
            mevcut.gelenMusteri.Add(yeniMusteri);   // Kiralama yapılacak durağı bulduktan sonra yeni müşteriyi durağa ekleme 
            mevcut.durak.bosPark++;
            if (mevcut.durak.normalBisiklet > 0)    // Duraktaki bisiklet ve park sayılarını güncelleme
                mevcut.durak.normalBisiklet--;
            else
                Console.WriteLine("Maalesef Kiralanacak Normal bisiklet Hiç bisiklet kalmadı");
        }
    }
    class Node
    {
        private Durak iData; 
        public Node(Durak key)  // Constructor
        {
            iData = key;
        }   
        public  Durak getKey()
        { return iData; }      
        public string print()   //Durak bilgilerini ekrana yazdıran metot
        {
            return "Durak Adı : "+iData.durakAdi+"\tBoş Park Sayısı : "+iData.bosPark+"\tTandem Bisiklet Sayısı : "+iData.tandemBisiklet+"\tNormal Bisiklet Sayısı : "+iData.normalBisiklet;
        }
        public void setKey(Durak id)
        { iData = id; }
    }
    class Heap
    {
        private Node[] heapArray;
        private int maxSize; // Dizi boyutu
        private int currentSize; 
        public Heap(int mx) // constructor
        {
            maxSize = mx;
            currentSize = 0;
            heapArray = new Node[maxSize]; // Maxsize boyutunda dizi oluşturur
        }
        public bool insert(Durak key)   // Durak ekleme metodu
        {
            if (currentSize == maxSize)
                return false;
            Node newNode = new Node(key);
            heapArray[currentSize] = newNode;
            trickleUp(currentSize++);
            return true;
        }
        public void trickleUp(int index)
        {
            int parent = (index - 1) / 2;
            Node bottom = heapArray[index];
            while (index > 0 && heapArray[parent].getKey().normalBisiklet < bottom.getKey().normalBisiklet) 
            {
                heapArray[index] = heapArray[parent]; // Aşağı taşır
                index = parent;
                parent = (parent - 1) / 2;
            }
            heapArray[index] = bottom;
        }
        public Node remove() // Maksimum değeri (Normal Bisiklet Sayısına göre) silip döndürür 
        { 
            Node root = heapArray[0];
            heapArray[0] = heapArray[--currentSize];
            trickleDown(0);
            return root;
        }
        public void trickleDown(int index)
        {
            int largerChild;
            Node top = heapArray[index]; // Kökü kaydeder
            while (index < currentSize / 2) 
            {   // en az bir çocuk
                int leftChild = 2 * index + 1;
                int rightChild = leftChild + 1;
                // daha büyük çocuğu bulur
                if (rightChild < currentSize && heapArray[leftChild].getKey().normalBisiklet <heapArray[rightChild].getKey().normalBisiklet)
                    largerChild = rightChild;
                else
                    largerChild = leftChild;               
                if (top.getKey().normalBisiklet >= heapArray[largerChild].getKey().normalBisiklet)
                    break;
                heapArray[index] = heapArray[largerChild];  // Çocuğu yukarı kaydırır
                index = largerChild;
            }
            heapArray[index] = top;
        }    
        public void displayHeap()   // Heap Arrayi yazdırır
        {
            Console.WriteLine("\nHeap Array :");
            for (int m = 0; m < currentSize; m++)
                if (heapArray[m] != null)
                    Console.WriteLine(heapArray[m].getKey().normalBisiklet );
                else
                    Console.WriteLine("--");
            Console.WriteLine();        
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Black;

            string[] duraklar = { "İnciraltı,  28, 2, 10", "Sahilevleri, 8, 1, 11", "Doğal Yaşam Parkı, 17, 1, 6", "Bostanlı İskele, 7, 0, 5", "Göztepe, 19, 2, 5", "Alsancak, 9, 2, 4", "Pasaport, 12, 3, 7", "Susuzdede, 14, 6, 8", "Karataş, 16, 4, 2" };
            DurakAgaci durakAgaci = new DurakAgaci();
            foreach (string s in duraklar)
            {
                string[] kelimeler = s.Split(',');
                Durak durak = new Durak(kelimeler);
                durakAgaci.Ekle(durak);
            }
            durakAgaci.InOrder(durakAgaci.getKok());     
            Console.WriteLine("\nDurak Ağacının Derinliği : " + durakAgaci.MaxDepth()+"\n");
            List<Durak> genericDuraklarListesi = new List<Durak>();
            foreach (string s in duraklar)
            {
                string[] kelimeler = s.Split(',');
                Durak durak = new Durak(kelimeler);
                genericDuraklarListesi.Add(durak);
            }
                  
            Console.WriteLine("Bulmak istediğiniz müşterinin ID'sini giriniz : ");
            int arananID = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("\nID'si : " + arananID + " Olan kişinin kiralama yaptığı durak bilgileri : \n-----------------------------------------------------------------------------------------------");
            durakAgaci.Bul(arananID);

            Console.WriteLine("\nKiralama yapmak istediğiniz durağın adını giriniz : ");
            string kiralamaDurak = Console.ReadLine();
            Console.WriteLine("\nKiralama yapacak müşterinin ID'sini giriniz : ");
            int kiralamaID = Convert.ToInt32(Console.ReadLine());           
            durakAgaci.kiralamaYap(kiralamaDurak, kiralamaID);
            Console.WriteLine("\nKiralama yapıldıktan sonraki güncel durak bilgileri : \n///////////////////////////////////////////////////////////////////////////////////////////////////////////////////");
            durakAgaci.InOrder(durakAgaci.kok);
                 
            Hashtable hashTable = new Hashtable();

            foreach (Durak durak in genericDuraklarListesi)
                hashTable.Add(durak.durakAdi, durak);
            Console.WriteLine("\nHastable : \n------------------------------------------------------------------------------------------------------------------------");
            foreach (Durak durak in hashTable.Values)
            {
                if (durak.bosPark > 5)
                    durak.normalBisiklet += 5;
                Console.WriteLine("Durak Adı : " + durak.durakAdi + "\tBoş Park Sayısı : " + durak.bosPark + "\tTandem Bisiklet Sayısı : " + durak.tandemBisiklet + "\tNormal Bisiklet Sayısı : " + durak.normalBisiklet);
            }

            Heap theHeap = new Heap(9);
            foreach (Durak durak in genericDuraklarListesi)
                theHeap.insert(durak);
           
            theHeap.displayHeap();

            Console.WriteLine("Sadece Normal Bisiklet Sayılarına göre MAX HEAP'teki ilk 3 durağın bilgileri : \n------------------------------------------------------------------------------------------------------------");
            for (int i = 0; i < 3; i++)
                Console.WriteLine(theHeap.remove().print() );                        
            //4
            int[] array = { 44, 22, 55, 77, 33, 66, 11, 99, 88 };
            Console.Write("\nOrijinal Dizi : \n");
            printArray(array);
            Console.WriteLine("\nSelection Sort ile sıralanmış hali : \n-------------------------------------");
            selectionSort(array);
            printArray(array);

            int n = array.Length;
            quickSort(array, 0, n - 1);
            Console.WriteLine("\nQuick Sort ile sıralanmış hali : \n-------------------------------------");
            printArray(array, n);
            Console.ReadKey();
        }   
        static void selectionSort(int[] arr)    // Selection Sort
        {
            int n = arr.Length;
            for (int i = 0; i < n - 1; i++)
            {
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                    if (arr[j] < arr[min_idx])
                        min_idx = j;
                int temp = arr[min_idx];
                arr[min_idx] = arr[i];
                arr[i] = temp;
            }
        }
        static void printArray(int[] arr)
        {
            int n = arr.Length;
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
        static int partition(int[] arr, int low,int high)   // Quick Sort
        {
            int pivot = arr[high];
            int i = (low - 1);
            for (int j = low; j < high; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    int temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }
            int temp1 = arr[i + 1];
            arr[i + 1] = arr[high];
            arr[high] = temp1;
            return i + 1;
        }
        static void quickSort(int[] arr, int low, int high)
        {
            if (low < high)
            {
                int pi = partition(arr, low, high);
                quickSort(arr, low, pi - 1);
                quickSort(arr, pi + 1, high);
            }
        }
        static void printArray(int[] arr, int n)
        {
            for (int i = 0; i < n; ++i)
                Console.Write(arr[i] + " ");
            Console.WriteLine();
        }
    }
}
