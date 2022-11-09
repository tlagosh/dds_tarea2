namespace EscobaServidor;

public class SubSetSum
{
  // dp[i][j] is going to store true if sum j is
  // possible with array elements from 0 to i.
  public bool[, ] dp;

  public List<List<int>> result = new List<List<int>>();
 
  public void display(List<int> v)
  {
    List<int> vCopy = new List<int>(v);
    result.Add(vCopy);
  }
 
  // A recursive function to print all subsets with the
  // help of dp[][]. Vector p[] stores current subset.
  public void printSubsetsRec(int[] arr, int i, int sum, List<int> p)
  {
    // If we reached end and sum is non-zero. We print
    // p[] only if arr[0] is equal to sum OR dp[0][sum]
    // is true.
    if (i == 0 && sum != 0 && dp[0, sum]) {
      p.Add(arr[i]);
      display(p);
      p.Clear();
      return;
    }
 
    // If sum becomes 0
    if (i == 0 && sum == 0) {
      display(p);
      p.Clear();
      return;
    }
 
    // If given sum can be achieved after ignoring
    // current element.
    if (dp[i - 1, sum]) {
      // Create a new vector to store path
      List<int> b = new List<int>();
      b.AddRange(p);
      printSubsetsRec(arr, i - 1, sum, b);
    }
 
    // If given sum can be achieved after considering
    // current element.
    if (sum >= arr[i] && dp[i - 1, sum - arr[i]]) {
      p.Add(arr[i]);
      printSubsetsRec(arr, i - 1, sum - arr[i], p);
    }
  }
 
  // Prints all subsets of arr[0..n-1] with sum 0.
  public void printAllSubsets(int[] arr, int n, int sum)
  {
    if (n == 0 || sum < 0)
      return;
 
    // Sum 0 can always be achieved with 0 elements
    dp = new bool[n, sum + 1];
    for (int i = 0; i < n; ++i) {
      dp[i, 0] = true;
    }
 
    // Sum arr[0] can be achieved with single element
    if (arr[0] <= sum)
      dp[0, arr[0]] = true;
 
    // Fill rest of the entries in dp[][]
    for (int i = 1; i < n; ++i)
      for (int j = 0; j < sum + 1; ++j)
        dp[i, j] = (arr[i] <= j)
        ? (dp[i - 1, j]
           || dp[i - 1, j - arr[i]])
        : dp[i - 1, j];
    if (dp[n - 1, sum] == false) {
      return;
    }
 
    // Now recursively traverse dp[][] to find all
    // paths from dp[n-1][sum]
    List<int> p = new List<int>();
    printSubsetsRec(arr, n - 1, sum, p);
  }
 
  // Driver Program to test above functions
  public List<List<int>> GetSubSetSums(int[] arr, int n, int sum)
  {
    printAllSubsets(arr, n, sum);
    return this.result;
  }
}
 
// This code is contributed by phasing17