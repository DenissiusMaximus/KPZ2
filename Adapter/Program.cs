var adapter = new FileLoggerAdapter(new FileWriter());

var client = new Client(adapter);
client.DoSomething();
client.DoSomethingElse();