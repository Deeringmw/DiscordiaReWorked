using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using System.Diagnostics;

namespace DiscordiaReWorked
{
    public class Program
    {

        private DiscordClient bot;
        private System.Timers.Timer stormbloodTimer;

        static void bot_MessageReceived(object sender, Discord.MessageEventArgs e)
        {
        }


        private static Job GetJobFromId(int? id)
        {
            if (id == null) return null;
            var client = new RestClient("http://jtmiles.xyz/aof/api");
            var requestJob = new RestRequest("job/{id}");
            requestJob.AddUrlSegment("id", id.ToString());
            var responseJob = client.Execute(requestJob);
            var contentJob = responseJob.Content;

            var theJob = JsonConvert.DeserializeObject<Job>(contentJob);

            return theJob;
        }

        public void StartTheBot()
        {
            var client = new RestClient("http://jtmiles.xyz/aof/api");
            bot = new Discord.DiscordClient();
            bot.MessageReceived += bot_MessageReceived;
            bot.UsingCommands(x =>
            {
                x.PrefixChar = '*';
                x.HelpMode = HelpMode.Public;
            });

            bot.GetService<CommandService>()
                .CreateCommand("events")
                .Parameter("blah", Discord.Commands.ParameterType.Unparsed)
                .Description("events")

                .Do(async e =>
                {
                    //Event Call
                    var requestEvent = new RestRequest("events");


                    var responseEvent = client.Execute(requestEvent);
                    var contentEvent = responseEvent.Content;
                    List<Event> theEvents = JsonConvert.DeserializeObject<List<Event>>(contentEvent); ;

                    foreach (var theEvent in theEvents)
                    {
                        var tank1job = GetJobFromId(theEvent.tank1);
                        var tank2job = GetJobFromId(theEvent.tank2);
                        var healer1job = GetJobFromId(theEvent.healer1);
                        var healer2job = GetJobFromId(theEvent.healer2);
                        var dps1job = GetJobFromId(theEvent.dps1);
                        var dps2job = GetJobFromId(theEvent.dps2);
                        var dps3job = GetJobFromId(theEvent.dps3);
                        var dps4job = GetJobFromId(theEvent.dps4);
                        var sb = new StringBuilder();

                        if (tank1job != null)
                        {
                            sb.Append($"\n :white_check_mark: :shield: {tank1job.name}");
                        }
                        else
                        {
                            sb.Append("\n :x: :shield: http://jtmiles.xyz/aof/home");
                        }
                        if (tank2job != null)
                        {
                            sb.Append($"\n :white_check_mark: :shield: {tank2job.name} ");
                        }
                        else
                        {
                            sb.Append("\n :x: :shield: http://jtmiles.xyz/aof/home");
                        }
                        if (healer1job != null)
                        {
                            sb.Append($"\n :white_check_mark: :heartpulse: {healer1job.name} ");
                        }
                        else
                        {
                            sb.Append("\n :x: :heartpulse: http://jtmiles.xyz/aof/home ");
                        }
                        if (healer2job != null)
                        {
                            sb.Append($"\n :white_check_mark: :heartpulse: {healer2job.name} ");
                        }
                        else
                        {
                            sb.Append("\n :x: :heartpulse: http://jtmiles.xyz/aof/home ");
                        }
                        if (dps1job != null)
                        {
                            sb.Append($"\n :white_check_mark: :crossed_swords: {dps1job.name} ");
                        }
                        else
                        {
                            sb.Append("\n :x: :crossed_swords: http://jtmiles.xyz/aof/home ");
                        }
                        if (dps2job != null)
                        {
                            sb.Append($"\n :white_check_mark: :crossed_swords: {dps2job.name} ");
                        }
                        else
                        {
                            sb.Append("\n :x: :crossed_swords: http://jtmiles.xyz/aof/home ");
                        }
                        if (dps3job != null)
                        {
                            sb.Append($"\n :white_check_mark: :crossed_swords: {dps3job.name} ");
                        }
                        else
                        {
                            sb.Append("\n :x: :crossed_swords: http://jtmiles.xyz/aof/home ");
                        }
                        if (dps4job != null)
                        {
                            sb.Append($"\n :white_check_mark: :crossed_swords: {dps4job.name} ");
                        }
                        else
                        {
                            sb.Append("\n :x: :crossed_swords: http://jtmiles.xyz/aof/home ");
                        }
                        await e.Channel.SendMessage($"**{theEvent.name}** {theEvent.start_time} {sb.ToString()}");

                    }

                });





            bot.GetService<CommandService>()
                        .CreateCommand("twitch")
                        .Parameter("game", Discord.Commands.ParameterType.Unparsed)
                        .Description("Twitch streams by game.")


                            .Do(async e =>
                            {
                                string gSearch = e.GetArg("game");
                                await e.Channel.SendMessage($"https://www.twitch.tv/directory/game/{gSearch}");
                            });

                        string topic = "not set! use *settopic to set a new topic!";
            bot.GetService<CommandService>()
                    .CreateCommand("settopic")
                    .Parameter("topic", Discord.Commands.ParameterType.Unparsed)
                    .Description("set topic")


                        .Do(async e =>
                        {
                            topic = e.GetArg("topic");
                            await e.Channel.SendMessage($"The topic has been set to {topic}");
                        });

            bot.GetService<CommandService>()
                    .CreateCommand("topic")
                    .Parameter("", Discord.Commands.ParameterType.Unparsed)
                    .Description("gets topic")


                        .Do(async e =>
                        {
                            await e.Channel.SendMessage($"The topic is currently {topic}");
                        });
             bot.GetService<CommandService>()
                    .CreateCommand("resettopic")
                    .Parameter("", Discord.Commands.ParameterType.Unparsed)
                    .Description("resets topic")
        
                        .Do(async e =>
                        {
                            topic = "not set! use '*settopic' to set a new topic!";
                        await e.Channel.SendMessage($"The topic has been reset. Use *settopic to set a new topic!");



                        });

            bot.GetService<CommandService>()
       .CreateCommand("signup")
       .Parameter("", Discord.Commands.ParameterType.Unparsed)
       .Description("links site")

           .Do(async e =>
           {
               
               await e.Channel.SendMessage($"http://jtmiles.xyz/aof/home");



           });








            if (Environment.UserInteractive)
            {
                bot.ExecuteAndWait(async () =>
                {
                    await bot.Connect("MjY3MDg4MDI1ODg2OTE2NjEx.DAjY8A.DgMN97nXgr9cOsCIqVXqLh67X6c", TokenType.Bot);
                    Console.WriteLine("Bot connected");
                });
            }
            else
            {
                bot.Connect("MjY3MDg4MDI1ODg2OTE2NjEx.DAjY8A.DgMN97nXgr9cOsCIqVXqLh67X6c", TokenType.Bot);
                
            }
        }


        //var server = bot.FindServers("bot test").FirstOrDefault();
        //var channel = server.FindChannels("general", ChannelType.Text).FirstOrDefault();




        static void Main(string[] args)
        {
            
                new Program().StartTheBot();
            
        }
    }
}
