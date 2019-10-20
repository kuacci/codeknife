# [179. Largest Number](https://leetcode.com/problems/largest-number/)

Given a list of non negative integers, arrange them such that they form the largest number.

**Example 1:**

> Input: [10,2]
> Output: "210"

**Example 2:**

> Input: [3,30,34,5,9]
> Output: "9534330"

Note: The result may be very large, so you need to return a string instead of an integer.

## 思路 - 桶排序

要构建一个值最大的数，就是要求值越大的数字排再越前越好。所有的数中9开头的数一定比8开头的大。例如9 和 8999排列，9在前面能得到最大的数, `98999 > 89999`.
先用一个10个元素的数组`List<string>[] = new List<string>[10]`来存放以0 - 9 开头的数字。
单个bucket里面的数再进行排序。排序的方式是将两个数字拼接起来看哪个大，大的则放在bueckt的前面，保证bucket内部有序。
输出的时候从大数开始输出，就能得到最后的答案。

## 代码 - 桶排序

