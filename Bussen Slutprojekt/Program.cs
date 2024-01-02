//Detta C#-program är en konsolapplikation för Bussreservations programmet. 
//Slutprojekt i programmering 1 - George Youssef, maj 2023.
using System;

public class Program
{
    public static void Main()
    {
        BusReservation busReservation = new BusReservation();

        while (true)
        {
            //Välkommen meddelande
            Console.WriteLine("\n============================================================");
            Console.WriteLine("         Välkommen till Bussreservations programmet         ");
            Console.WriteLine("============================================================\n");
            Console.WriteLine("Nu har du fyra val:"); //Info för användaren
            Console.WriteLine("--------------------------");
            // visa menyn
            Console.WriteLine("\n1. Lägg till passagerare");//Fyra val för användaren
            Console.WriteLine("\n2. Avbokning");
            Console.WriteLine("\n3. Visa statistik");
            Console.WriteLine("\n4. Avsluta");
            Console.WriteLine("\n--------------------------");//Dekoration

            // fråga användaren om sitt val           
            int choice;
            Console.Write("Ange ditt val (1-4): ");
            bool validInput = int.TryParse(Console.ReadLine(), out choice);

            if (!validInput)
            {
                Console.WriteLine("\nFelaktig inmatning!\n"); //Meddelande visas om användaren skriver bokstäver och inte siffror
                continue; //För att försätta koden utan avbrott
            }

            switch (choice)
            {
                case 1:
                    busReservation.AddPassenger();
                    break;//För att gå direkt till nästa steg

                case 2:
                    busReservation.CancelBooking();
                    break;

                case 3:
                    busReservation.DisplayStats();
                    break;

                case 4://Meddelande vid avslutning av programmet
                    Console.WriteLine("\n===========================================================================");
                    Console.WriteLine("=        Det här programmet är mitt slutprojekt i programmering 1         =");
                    Console.WriteLine("=                Tack att du har använt programmet. Hejdå!                =");
                    Console.WriteLine("=                        (C) George Youssef, maj 2023                     =");
                    Console.WriteLine("===========================================================================");

                    Console.WriteLine("\nTryck Enter för att avsluta ... ");
                    Console.ReadLine();//För att använfaren kunde läsa dett sista meddelande
                    return;

                default:
                    Console.WriteLine("\n-----------------------------------------");
                    Console.WriteLine("Ogiltigt val! Ange ett giltigt val (1-4)\n"); //Meddelande visas om användren svriver ett siffra som är inte imellan av 1-4
                    continue;
            }
        }
    }
}


public class Passenger //Val 1
{
    public string name;
    public int age;
    public char gender;
    public bool isBusinessClass;

    // konstruktör för att initiera passagerarobjektet
    public Passenger(string name, int age, char gender, bool isBusinessClass)
    {
        this.name = name;
        this.age = age;
        this.gender = gender;
        this.isBusinessClass = isBusinessClass;
    }
}

public class BusReservation
{
    private Passenger[] passengers;
    private int totalPassengers;
    private int maleCount;
    private int femaleCount;
    private int otherCount;
    private int totalAge;
    private int businessSeatsTaken;
    private int economySeatsTaken;

    // konstruktör för att initiera bussreservationsobjekte
    public BusReservation()
    {
        passengers = new Passenger[50]; // 50 platser i bussen, förta 10 platser är BusinessKlass och resten är EconomyKlass
        totalPassengers = 0;
        maleCount = 0;
        femaleCount = 0;
        otherCount = 0;
        totalAge = 0;
        businessSeatsTaken = 0;
        economySeatsTaken = 0;
    }

    // metod för att lägga till en passagerare i bussreservationen
    public void AddPassenger()
    {
        // kontrollera om det finns lediga platser
        if (totalPassengers >= 50)
        {
            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("Tyvärr, bussen är full!");
            return;
        }

        // be användaren om passagerar information      
        Console.WriteLine("\n----------------------------------------------------");
        Console.WriteLine("                Lägg till passagerare              ");
        Console.WriteLine("----------------------------------------------------");

        Console.Write("\nAnge passagerarens namn: ");
        string name = Console.ReadLine();

        Console.Write("\nAnge passagerarens ålder: ");
        int age = Convert.ToInt32(Console.ReadLine());

        char gender; // Denna kod för att användaren MÅSTE skriva M eller F eller A; annars visar meddelande och köra om koden:
        do
        {
            Console.Write("\nAnge passagerarens kön - Man / Kvinna / Annat - (M/F/A): ");
            gender = Convert.ToChar(Console.ReadLine().ToUpper());

            if (gender != 'M' && gender != 'F' && gender != 'A')
            {
                Console.WriteLine("Ogiltigt kön. Vänligen ange 'M', 'F' eller 'A'"); //Meddelande visas
            }
        } while (gender != 'M' && gender != 'F' && gender != 'A');

        char seatClass; // Denna kod för att användaren MÅSTE skriva B ller E, annars visar meddelande och köra om koden:
        do
        {
            Console.Write("\nAnge passagerarsätesklass (B/E): ");// B för BusinessClass och E för EconomyClass
            seatClass = Convert.ToChar(Console.ReadLine().ToUpper());

            if (seatClass != 'B' && seatClass != 'E')
            {
                Console.WriteLine("Ogiltigt sätesklass. Vänligen ange 'B' eller 'E'.");
            }
        } while (seatClass != 'B' && seatClass != 'E');

        bool isBusinessClass = (seatClass == 'B');// Bestäm om passageraren är i Business Klass genom att kontrollera om sätesklassen som anges är 'B'

        // skapa ett nytt passagerarobjekt med de angivna uppgifterna
        Passenger passenger = new Passenger(name, age, gender, isBusinessClass);

        // lägg till passageraren i passagerargruppen
        passengers[totalPassengers] = passenger;
        totalPassengers++;

        // uppdatera statistik
        if (gender == 'M')
        {
            maleCount++;
        }
        else if (gender == 'F')
        {
            femaleCount++;
        }
        else
        {
            otherCount++;
        }


        totalAge += age;

        if (isBusinessClass)
        {
            businessSeatsTaken++;
        }
        else
        {
            economySeatsTaken++;
        }

        Console.WriteLine("\n------------------------------------");
        Console.WriteLine("Passagerare tillagd framgångsrikt!\n");
    }


    // metoden för att avboka en bokning
    public void CancelBooking()
    {
        // be användaren om sittplatsnumret för att avboka
        Console.WriteLine("\n----------------------------------------------------");
        Console.WriteLine("                     AVBOKNING !                   ");
        Console.WriteLine("----------------------------------------------------");

        Console.Write("\nAnge platsnummer för att avboka (1-50): ");
        int seatNumber = Convert.ToInt32(Console.ReadLine());

        bool isFound = false;

        // sök efter passageraren med det angivna sätesnumret
        for (int i = 0; i < totalPassengers; i++)
        {
            if (passengers[i] != null && (i + 1) == seatNumber)
            {
                // uppdatera statistik
                if (passengers[i].gender == 'M')
                {
                    maleCount--;
                }
                else if (passengers[i].gender == 'F')
                {
                    femaleCount--;
                }
                else
                {
                    otherCount--;
                }

                totalAge -= passengers[i].age;

                if (passengers[i].isBusinessClass)
                {
                    businessSeatsTaken--;
                }
                else
                {
                    economySeatsTaken--;
                }

                passengers[i] = null;
                totalPassengers--;
                isFound = true;

                Console.WriteLine("\n------------------------------------");
                Console.WriteLine("Bokningen avbröts framgångsrikt!");
                break;
            }
        }

        if (!isFound)
        {
            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("Platsnummer hittades inte!");
        }
    }

    // metod för att visa bussbokningsstatistiken
    public void DisplayStats()
    { // kontrollera om några passagerare har lagts till
        if (totalPassengers == 0)
        {
            Console.WriteLine("\n------------------------------------");
            Console.WriteLine("Inga passagerare har lagts till ännu!");
            return;
        }

        // beräkna och visa statistik
        double averageAge = (double)totalAge / totalPassengers;
        Console.WriteLine("\n----------------------------------------------------");
        Console.WriteLine("                      Statistik                    ");
        Console.WriteLine("----------------------------------------------------");

        Console.WriteLine("\nMedelålder för passagerare: " + averageAge.ToString("0.00"));

        Console.WriteLine("\nAntal 'Manliga' passagerare: " + maleCount);
        Console.WriteLine("\nAntal 'Kvinnliga' passagerare: " + femaleCount);
        Console.WriteLine("\nAntal 'Annat kön' passagerare: " + otherCount);

        Console.WriteLine("\nAntal upptagna platser i businessklass: " + businessSeatsTaken);
        Console.WriteLine("\nAntal platser i ekonomiklass: " + economySeatsTaken);
    }
}