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
                            sb.Append($"\n Tank {tank1job.name}");
                        }
                        else
                        {
                            sb.Append("\n [][][] Tank spot open  ");
                        }
                        if (tank2job != null)
                        {
                            sb.Append($"\n Tank {tank2job.name} ");
                        }
                        else
                        {
                            sb.Append("\n [][][] Tank Spot Open ");
                        }
                        if (healer1job != null)
                        {
                            sb.Append($"\n Healer {healer1job.name} ");
                        }
                        else
                        {
                            sb.Append("\n +++++ Healer Spot Open ");
                        }
                        if (healer2job != null)
                        {
                            sb.Append($"\n Healer {healer2job.name} ");
                        }
                        else
                        {
                            sb.Append("\n +++++ Healer Spot Open ");
                        }
                        if (dps1job != null)
                        {
                            sb.Append($"\n DPS {dps1job.name} ");
                        }
                        else
                        {
                            sb.Append("\n ----- DPS Spot Open ");
                        }
                        if (dps2job != null)
                        {
                            sb.Append($"\n DPS {dps2job.name} ");
                        }
                        else
                        {
                            sb.Append("\n ----- DPS Spot Open ");
                        }
                        if (dps3job != null)
                        {
                            sb.Append($"\n DPS {dps3job.name} ");
                        }
                        else
                        {
                            sb.Append("\n ----- DPS Spot Open ");
                        }
                        if (dps4job != null)
                        {
                            sb.Append($"\n DPS {dps4job.name} ");
                        }
                        else
                        {
                            sb.Append("\n ----- DPS Spot Open ");
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
               await e.Channel.SendMessage("!say !say");
           });

            bot.GetService<CommandService>()
.CreateCommand("alltimes")
.Parameter("null", Discord.Commands.ParameterType.Unparsed)
.Description("all timers")


.Do(async e =>
{
    

    DateTime rmRelease = new DateTime(2017, 7, 30, 21, 0, 0, 0);
    TimeSpan rmDiff = rmRelease - DateTime.Now;
    string rmFormatted = string.Format(
                       "Rick and Morty: {0} days, {1} hours, {2} minutes, {3} seconds!",
                       rmDiff.Days,
                       rmDiff.Hours,
                       rmDiff.Minutes,
                       rmDiff.Seconds);
    await e.Channel.SendMessage(rmFormatted);

    DateTime gotRelease = new DateTime(2017, 7, 16, 21, 0, 0, 0);
    TimeSpan gotDiff = gotRelease - DateTime.Now;
    string gotString = string.Format(
                           "Game of Thrones: {0} days, {1} hours, {2} minutes, {3} seconds!",
                           gotDiff.Days,
                           gotDiff.Hours,
                           gotDiff.Minutes,
                           gotDiff.Seconds);
    await e.Channel.SendMessage(gotString);


});


            

            bot.GetService<CommandService>()
    .CreateCommand("gotcountdown")
    .Parameter("blah", Discord.Commands.ParameterType.Unparsed)
    .Description("Time until Stormblood")
    .Alias("got")

    .Do(async e =>
    {

        DateTime release = new DateTime(2017, 7, 16, 21, 0, 0, 0);
        TimeSpan diff = release - DateTime.Now;
        string formatted = string.Format(
                               "{0} days, {1} hours, {2} minutes, {3} seconds until Game of Thrones!",
                               diff.Days,
                               diff.Hours,
                               diff.Minutes,
                               diff.Seconds);
        await e.Channel.SendMessage(formatted);
    });

            bot.GetService<CommandService>()


.CreateCommand("botting")
.Parameter("blah", Discord.Commands.ParameterType.Unparsed)
.Description("Time until Stormblood")
.Alias("rm")

.Do(async e =>
{

    Process[] pname = Process.GetProcessesByName("notepad");
    if (pname.Length == 0)
        await e.Channel.SendMessage("Botting! Filthy dirty Cheat.");
    else
        await e.Channel.SendMessage("All clear!");

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
                stormbloodTimer.Start();
            }
        }


        //var server = bot.FindServers("bot test").FirstOrDefault();
        //var channel = server.FindChannels("general", ChannelType.Text).FirstOrDefault();




        static void Main(string[] args)
        {
            if (Environment.UserInteractive)
            {
                new Program().StartTheBot();
            }
        }
    }
}
