The Bank of Shire.

Greatings dear sir or madam! I'm writing to you with a big request: I would like you to implement a host service for our Bank of Shire. 
We hobbits are simple folk and we don't want anything fancy. We don't charge or pay interest, we just want something where we can store our money and borrow in time of need - it is all based on trust here you see...
We gave this task to one of our young hobbits, but he went off to kill some dragon (can you imagine that? I bet it is the Tuk blood in his veins!). He was in hurry, so he didn't leave any documentation, or comments in the code, but he did leave you a test application (good lad!) - you should figure out everything you need from it.

Summary: 
1. Please try not to change any code in the 'CustomerTest' application unless you have to due to bug. If you have to fix a bug in there please add a comment.
2. The 'CustomerTest' application should execute without throwing any exceptions.
3. Feel free to change any other code (including shared interface if you need).
4. The 'CustomerTest' application should print out transaction history for each customer in a human readable form. 

5. Write an 'Inspector' application that will block customer operations during inspection and print out summary of all customers operations. Please use the InspectorInterface. 
It should work like that:
StartInspection()
...
GetFullSummary()
Console.ReadKey()
FinishInspection() 