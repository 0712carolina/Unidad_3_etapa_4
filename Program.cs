//Audry Carolina Giraldo Trujillo
using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main(string[] args)
    {
        try
        {
            SortingApplication app = new SortingApplication();
            app.Run();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}

class SortingApplication
{
    private int[] numbers;

    public void Run()
    {
        Console.WriteLine("Welcome to the Sorting Application!");
        Console.WriteLine("You will enter 10 unique numbers.");

        InputNumbers();
        DisplayMenu();
    }

    private void InputNumbers()
    {
        numbers = new int[10];
        Console.WriteLine("Please enter 10 different numbers:");

        for (int i = 0; i < 10; i++)
        {
            bool isValidInput = false;
            while (!isValidInput)
            {
                try
                {
                    Console.Write($"Enter number {i + 1}: ");
                    int input = int.Parse(Console.ReadLine());

                    if (numbers.Contains(input))
                    {
                        Console.WriteLine("The number must be unique. Please try again.");
                        continue;
                    }

                    numbers[i] = input;
                    isValidInput = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }
            }
        }
    }

    private void DisplayMenu()
    {
        Console.WriteLine("\nChoose a sorting method:");
        Console.WriteLine("1. Bubble Sort");
        Console.WriteLine("2. Shell Sort");
        Console.WriteLine("3. Selection Sort");
        Console.WriteLine("4. Insertion Sort");
        Console.Write("Enter your choice (1-4): ");

        try
        {
            int choice = int.Parse(Console.ReadLine());
            SortNumbers(choice);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Invalid choice: {ex.Message}");
        }
    }

    private void SortNumbers(int choice)
    {
        int[] sortedNumbers;

        switch (choice)
        {
            case 1:
                sortedNumbers = SortingAlgorithms.BubbleSort((int[])numbers.Clone());
                Console.WriteLine("Numbers sorted using Bubble Sort:");
                break;
            case 2:
                sortedNumbers = SortingAlgorithms.ShellSort((int[])numbers.Clone());
                Console.WriteLine("Numbers sorted using Shell Sort:");
                break;
            case 3:
                sortedNumbers = SortingAlgorithms.SelectionSort((int[])numbers.Clone());
                Console.WriteLine("Numbers sorted using Selection Sort:");
                break;
            case 4:
                sortedNumbers = SortingAlgorithms.InsertionSort((int[])numbers.Clone());
                Console.WriteLine("Numbers sorted using Insertion Sort:");
                break;
            default:
                Console.WriteLine("Invalid choice. Exiting program.");
                return;
        }

        Console.WriteLine(string.Join(", ", sortedNumbers));
        SaveToFile(sortedNumbers);
    }

    private void SaveToFile(int[] sortedNumbers)
    {
        Console.WriteLine("\nDo you want to save the sorted numbers to a file? (yes/no):");
        string saveToFile = Console.ReadLine().ToLower();

        if (saveToFile == "yes")
        {
            try
            {
                File.WriteAllText("SortedNumbers.txt", string.Join(", ", sortedNumbers));
                Console.WriteLine("Numbers saved to SortedNumbers.txt");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to save file: {ex.Message}");
            }
        }
    }
}

static class SortingAlgorithms
{
    public static int[] BubbleSort(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            for (int j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    int temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
        return arr;
    }

    public static int[] ShellSort(int[] arr)
    {
        int n = arr.Length;
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i++)
            {
                int temp = arr[i];
                int j;
                for (j = i; j >= gap && arr[j - gap] > temp; j -= gap)
                {
                    arr[j] = arr[j - gap];
                }
                arr[j] = temp;
            }
        }
        return arr;
    }

    public static int[] SelectionSort(int[] arr)
    {
        for (int i = 0; i < arr.Length - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < arr.Length; j++)
            {
                if (arr[j] < arr[minIndex])
                {
                    minIndex = j;
                }
            }
            int temp = arr[minIndex];
            arr[minIndex] = arr[i];
            arr[i] = temp;
        }
        return arr;
    }

    public static int[] InsertionSort(int[] arr)
    {
        for (int i = 1; i < arr.Length; i++)
        {
            int key = arr[i];
            int j = i - 1;

            while (j >= 0 && arr[j] > key)
            {
                arr[j + 1] = arr[j];
                j--;
            }
            arr[j + 1] = key;
        }
        return arr;
    }
}

