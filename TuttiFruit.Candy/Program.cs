using System;
using System.Threading.Channels;

namespace TuttiFruit.Candy
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = Channel.CreateBounded<object>(10);
            var consumer = new Consumer(channel.Reader);

            _ = consumer.StartConsumeAsync();

            var kafkaSubscriber = new KafkaSubscriber();
            var rabbitSubscriber = new RabbitSubscriber();
            var ibmSubscriber = new IBMSubscriber();
            var redisSubscriber = new RedisSubscriber();

            var kafkaProducer = new Producer(channel.Writer, kafkaSubscriber);
            var rabbitProducer = new Producer(channel.Writer, rabbitSubscriber);
            var ibmProducer = new Producer(channel.Writer, ibmSubscriber);
            var redisProducer = new Producer(channel.Writer, redisSubscriber);

            //Simulate message broker
            kafkaSubscriber.PublishMessage("Message from Kafka");
            rabbitSubscriber.PublishMessage("Message from rabbit");
            ibmSubscriber.PublishMessage("Message from IBM");
            redisSubscriber.PublishMessage("Message from Redis");

            //kafkaProducer.Complete();
            //rabbitProducer.Complete();
            //ibmProducer.Complete();
            //redisProducer.Complete();

            Console.ReadKey();
        }
    }
}
