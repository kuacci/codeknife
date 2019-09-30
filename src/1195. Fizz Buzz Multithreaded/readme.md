# 1195. Fizz Buzz Multithreaded

Write a program that outputs the string representation of numbers from 1 to n, however:

* If the number is divisible by 3, output "fizz".
* If the number is divisible by 5, output "buzz".
* If the number is divisible by both 3 and 5, output "fizzbuzz".
For example, for `n = 15`, we output: `1, 2, fizz, 4, buzz, fizz, 7, 8, fizz, buzz, 11, fizz, 13, 14, fizzbuzz`.

Suppose you are given the following code:

class FizzBuzz {
  public FizzBuzz(int n) { ... }               // constructor
  public void fizz(printFizz) { ... }          // only output "fizz"
  public void buzz(printBuzz) { ... }          // only output "buzz"
  public void fizzbuzz(printFizzBuzz) { ... }  // only output "fizzbuzz"
  public void number(printNumber) { ... }      // only output the numbers
}
Implement a multithreaded version of FizzBuzz with **four** threads. The same instance of FizzBuzz will be passed to four different threads:

1. Thread A will call fizz() to check for divisibility of 3 and outputs fizz.
1. Thread B will call buzz() to check for divisibility of 5 and outputs buzz.
1. Thread C will call fizzbuzz() to check for divisibility of 3 and 5 and outputs fizzbuzz.
1. Thread D will call number() which should only output the numbers.

## 思路

这是一道多线程的问题。输出的要求倒是简单，输入的值为`int`型, 从1一直自加到n为止，当这个值符合上述某个条件时候，输出对应的字串：

* If the number is divisible by 3, output "fizz".
* If the number is divisible by 5, output "buzz".
* If the number is divisible by both 3 and 5, output "fizzbuzz".
* else, output the number

这里的问题在于, 实现上述4中条件的method需要由4个线程分别执行，而i的值只有1个，由4个线程共享。因此需要考虑多线程时候的线程同步问题。即，i一直在自加，但是同一时间应该只有一个线程在输出，而另外几个线程则不会有输出。

这种场景的需求可以使用 [System.Threading.Barrier](https://docs.microsoft.com/en-us/dotnet/api/system.threading.barrier?view=netframework-4.8)

## 代码

```csharp
public class FizzBuzz {
    private int n;
    private int i;
    private System.Threading.Barrier barrier;

    public FizzBuzz(int n) {
        this.n = n;
        i = 0;
        barrier = new System.Threading.Barrier(4);
    }

    // printFizz() outputs "fizz".
    public void Fizz(Action printFizz) {
        while(true)
        {
            barrier.SignalAndWait();
            if(i > n)
                break;
            if(i % 3 == 0 && i % 5 != 0)
                printFizz();
            barrier.SignalAndWait();
        }
    }

    // printBuzzz() outputs "buzz".
    public void Buzz(Action printBuzz) {
        while(true)
        {
            barrier.SignalAndWait();
            if(i > n)
                break;
            if(i % 5 == 0 && i % 3 != 0)
                printBuzz();
            barrier.SignalAndWait();
        }
    }

    // printFizzBuzz() outputs "fizzbuzz".
    public void Fizzbuzz(Action printFizzBuzz) {
        while(true)
        {
            barrier.SignalAndWait();
            if(i > n)
                break;
            if(i % 5 == 0 && i % 3 == 0)
                printFizzBuzz();
            barrier.SignalAndWait();
        }
    }

    // printNumber(x) outputs "x", where x is an integer.
    public void Number(Action<int> printNumber) {

        while(true)
        {
            i++;
            barrier.SignalAndWait();
            if(i > n)
                break;
            if(i % 3 != 0 && i % 5 != 0)
                printNumber(i);
            barrier.SignalAndWait();
        }

    }
}
```
