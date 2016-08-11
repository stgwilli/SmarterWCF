# SmarterWCF
Making WCF Smarter with the power of Roslyn


## Backstory
I was reading [this](http://blogs.msmvps.com/p3net/2014/02/02/a-smarter-wcf-service-client-part-1/) blog post series on how to make WCF smarter. I really liked all the ideas...except the T4 templates. So I though: 'Why not use Rosyln instead?' So that's what this project is.


## How does it work?
It's quite easy! Use the `ServiceChannelClientFactory` to build the WCF proxy and call the methods on the proxy as you would on a regular class. The proxy will handle all the cleanup for you.

### The Service Contract
```c#
  [ServiceContract]
  public interface IAddThings
  {
    [OperationContract]
    AddResult Add(AddRequest request);
  }
```
### Client Usage
```c#
    var client = new ServiceChannelClientFactory().Build<IAddThings>();
    var result = client.Add(new AddRequest { FirstNumber = 6, SecondNumber = 6 });
    Console.WriteLine($"Result {result.Result}");

    Console.ReadLine();
```
