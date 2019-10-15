# [151. Reverse Words in a String](https://leetcode.com/problems/reverse-words-in-a-string/)

Given an input string, reverse the string word by word.

**Example 1:**

> Input: "the sky is blue"
> Output: "blue is sky the"

**Example 2:**

> Input: "  hello world!  "
> Output: "world! hello"
> Explanation: Your reversed string should not contain leading or trailing spaces.

**Example 3:**

> Input: "a good   example"
> Output: "example good a"
> Explanation: You need to reduce multiple spaces between two words to a single space in the reversed string.

Note:

> A word is defined as a sequence of non-space characters.
> Input string may contain leading or trailing spaces. However, your reversed string should not contain leading or trailing spaces.
> You need to reduce multiple spaces between two words to a single space in the reversed string.

Follow up:

For C programmers, try to solve it in-place in O(1) extra space.

## 思路 - StringBuilder

这道题目的要求是将整个单词进行翻转，单词之间由`' '`来间隔。
为了达到翻转的效果，先将s用`' '`来分拆成为string[]. 这样就知道由多个个单词。用string.Trim()方法可以取出头尾的`' '`。同时在翻转的时候，判断是否是`' '`，如果是的话就跳过。
为了提高string拼接的效率，使用StringBuilder.

## 代码 - StringBuilder

```csharp

```
