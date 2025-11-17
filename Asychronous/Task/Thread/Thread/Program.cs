
void WriteThreadId()
{
    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);
}

//WriteThreadId();

Thread thread1 = new Thread(WriteThreadId);
thread1.Start();

//为了不直接退出程序
Console.ReadLine();