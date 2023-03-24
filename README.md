# AWS Lambda Language vs. Performance #

Let's compare the AWS Lambda Performance of four popular languages:
* Node.js (v18.x)
* Python (v3.9)
* C# (.NETCore 6)
* Go (v1.20.2)

Each will make a call to the same MongoDB database and return 100 elements.
We will use K6 to compare the performance of the implementations. 

