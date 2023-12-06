﻿int[] randomArray = new int[]
{
    42, 17, 8, 34, 25, 19, 11, 3, 38, 51,
    22, 6, 29, 14, 47, 33, 9, 55, 2, 27,
    13, 46, 31, 4, 16, 37, 7, 28, 20, 12,
    11, 6, 19, 22, 8, 14, 33
};

void PrintArray(int[] array)
{
    foreach (int value in array)
    {
        Console.Write(value + " ");
    }

    Console.WriteLine();
}

// -------------SORTINGS------------- //
// --Expand a function to read more-- //

// Time: O(n^2)
// Space: O(1)
void SelectionSort(int[] Array)
{
    /*
    The idea of this sort is: Loop through the array, find the smallest element, put it to the first place.
    Then Loop through the array (this time from the 2nd position because the first one is the smallest), find
    the second smallest, put it after the first... And so on
    */

    int minIndex;

    for (int i = 0; i < Array.Length; i++)
    {
        minIndex = i;

        for (int j = i + 1; j < Array.Length; j++)
        {
            // Replace min_index if a smaller element is found
            if (Array[j] < Array[minIndex])
            {
                minIndex = j;
            }
        }

        // Now swap the smallest element found (min_index) with the current position (i)
        int tmp = Array[minIndex];
        Array[minIndex] = Array[i];
        Array[i] = tmp;
    }
}

void BubbleSort(int[] Array)
{
    /*
    The idea is to swap adjacent elements while looping through the array. After each iteration, the
    biggest element (then 2nd biggest, then 3rd biggest... so on) will be "drowned" to the end of the array.
    We will repeat until there is an iteration where we do not perform any swap, which means the array is sorted.
    */

    bool isSwapped = true;

    // Perform if there is still some "swaps" in the previous iteration
    while (isSwapped)
    {
        // Reset the swap to false, set back to true if we have to perform a swap
        isSwapped = false;

        // Because we will compare Array[i] with Array[i + 1], therefor only go to Length - 1 or we will
        // get out of the array bound
        for (int i = 0; i < Array.Length - 1; i++)
        {
            if (Array[i] > Array[i + 1])
            {
                // Set swap to true
                isSwapped = true;
                // Swap
                int tmp = Array[i];
                Array[i] = Array[i + 1];
                Array[i + 1] = tmp;
            }
        }
    }
}

void InsertionSort(int[] Array)
{
    /*
    We will loop through the array only once, on each element, we will swap it to its correct position.
    This case the array from index 0 to i - 1 will be sorted, and we will have to put array[i] to its
    position (somewhere between 0 and i)

    Watch the visual example here to understand more
    (https://leetcode.com/explore/learn/card/sorting/694/comparison-based-sorts/4435/)
    */

    // We start with i = 1 because we will compare it with i - 1
    for (int i = 1; i < Array.Length; i++)
    {
        // Current position
        int currentIndex = i;

        // Swap it to the correct position
        while (currentIndex > 0 && Array[currentIndex] < Array[currentIndex - 1])
        {
            int tmp = Array[currentIndex];
            Array[currentIndex] = Array[currentIndex - 1];
            Array[currentIndex - 1] = tmp;

            // Decrese current index until it reaches 1
            currentIndex--;
        }
    }
}

// Time: O(nlogn)
// Space: O(n)
void MergeSort(int[] Array)
{
    /*
        Recursively divide the array into smaller arrays, sort the smaller subarrays and
        then merge the sorted array back
    */

    // Stop recursion
    if (Array.Length <= 1)
    {
        return;
    }
    // Divide into smaller subarrays
    else
    {
        int middleIndex = Array.Length / 2;

        // Create new subarrays 0 1 2 - 3 4 5 6
        int[] leftArray = new int[middleIndex];
        int[] rightArray = new int[Array.Length - middleIndex];

        // Copy the elements from the original array
        int i = 0;
        int j = 0;
        for (; i < Array.Length; i++)
        {
            if (i < middleIndex)
            {
                leftArray[i] = Array[i];
            }
            else
            {
                rightArray[j] = Array[i];
                j++;
            }
        }

        // Recursively call the MergeSort
        MergeSort(leftArray);
        MergeSort(rightArray);
        // After that, merge the 2 merged subarrays
        Merge(leftArray, rightArray, Array);
    }
}

// Merge: Helper function for the MergeSort
void Merge(int[] LeftSubarray, int[] RightSubarray, int[] MergedArray)
{
    // This function merge 2 merged arrays
    int leftLength = LeftSubarray.Length;
    int rightLength = RightSubarray.Length;

    int i = 0;
    int j = 0;
    int a = 0;

    while (i < leftLength && j < rightLength)
    {
        if (LeftSubarray[i] < RightSubarray[j])
        {
            MergedArray[a] = LeftSubarray[i];
            i++;
        }
        else
        {
            MergedArray[a] = RightSubarray[j];
            j++;
        }
        a++;
    }

    if (i == leftLength)
    {
        while (j < rightLength)
        {
            MergedArray[a] = RightSubarray[j];
            j++;
            a++;
        }
    }
    else if (j == rightLength)
    {
        while (i < leftLength)
        {
            MergedArray[a] = LeftSubarray[i];
            i++;
            a++;
        }
    }
}

// Counting Sort
void CountingSort(int[] Array)
{
    // Find the max element from the array
    int max = Array[0];

    for (int i = 1; i < Array.Length; i++)
    {
        if (Array[i] > max)
        {
            max = Array[i];
        }
    }

    // Initialize an array of max+1 elements with all elems = 0
    int[] Count = new int[max+1];

    // Store the count of each unique element of the input array at their respective indices
    for (int i = 0; i < Array.Length; i++)
    {
        Count[Array[i]] += 1;
    }

    // Store the cumulative sum or prefix sum of the elements of the countArray[] by doing countArray[i] = countArray[i – 1] + countArray[i]. This will help in placing the elements of the input array at the correct index in the output array.
    for (int i = 1; i < Count.Length; i++)
    {
        Count[i] = Count[i - 1] + Count[i];
    }

    // Create a new SortedArray
    int[] Sorted = new int[Array.Length];

    // Traverse from the end of the input array
    // Update outputArray[ countArray[ inputArray[i] ] – 1] = inputArray[i]. Also, update countArray[ inputArray[i] ] = countArray[ inputArray[i] ]–
    for (int i = Array.Length - 1; i >= 0; i--)
    {
        // Place the elem to the Sorted array
        Sorted[Count[Array[i]] - 1] = Array[i];
        // Update the Prefix sum
        Count[Array[i]]--;
    }

    // Watch here: https://www.youtube.com/watch?v=EItdcGhSLf4

    // Return the result
    for (int i = 0; i < Array.Length; i++)
    {
        Array[i] = Sorted[i];
    }   
}

// ------------SEARCHINGS------------ //
// --Expand a function to read more-- //



// PROGRAM STARTS HERE

// Original array
PrintArray(randomArray);
// Algorithm
CountingSort(randomArray);
// Output
Console.WriteLine("Sorted array");
PrintArray(randomArray);