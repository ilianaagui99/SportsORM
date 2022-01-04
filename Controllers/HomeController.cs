using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using SportsORM.Models;


namespace SportsORM.Controllers
{
    public class HomeController : Controller
    {

        private static Context _context;

        public HomeController(Context DBContext)
        {
            _context = DBContext;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            ViewBag.BaseballLeagues = _context.Leagues
                .Where(l => l.Sport.Contains("Baseball"))
                .ToList();
            return View();
        }

        [HttpGet("level_1")]
        public IActionResult Level1()
        {
             // ViewBag.<Name of bag> = context.<Name of table>.<Your query goes here> 

            // All womens' leagues
            ViewBag.WomenLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Womens"))
                .ToList();
            //All leagues of hockey
            ViewBag.HockeyLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Hockey"))
                .ToList();
            //All leagues that are not football
            ViewBag.NotFootballLeagues = _context.Leagues
                .Where(l => ! l.Name.Contains("Football"))
                .ToList();
            //All leagues called conferences
            ViewBag.ConferenceLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Conference"))
                .ToList();
            //All leagues in Atlantic region
            ViewBag.AtlanticLeagues = _context.Leagues
                .Where(l => l.Name.Contains("Atlantic"))
                .ToList();
            
            //All teams in Dallas
            ViewBag.DallasTeams = _context.Teams
                .Where(l => l.Location.Contains("Dallas"))
                .ToList();
            //All teams named Raptors
            ViewBag.RaptorsTeams = _context.Teams
                .Where(l => l.TeamName.Contains("Raptors"))
                .ToList();
            //all teams whos location includes city
            ViewBag.CityTeams = _context.Teams
                .Where(l => l.Location.Contains("City"))
                .ToList();
            //all teams who start with T
            ViewBag.TTeams = _context.Teams
                .Where(l => l.TeamName.StartsWith("T"))
                .ToList();
            //all teams ordered alphabetically by location
            ViewBag.byLocationTeams = _context.Teams
                .OrderBy(l => l.Location)
                .ToList();
            //all teams order by team name descending
            ViewBag.byNameTeams = _context.Teams
                .OrderByDescending(l => l.TeamName)
                .ToList();
            
            //players with last name cooper
            ViewBag.Coopers = _context.Players
                .Where(l => l.LastName == "Cooper")
                .ToList();
            //players w first name Joshua
            ViewBag.Joshuas = _context.Players
                .Where(l => l.FirstName == "Joshua")
                .ToList();
            //players w last name cooper but not joshua 
            ViewBag.noJCoopers = _context.Players
                .Where(l => l.FirstName != "Joshua" && l.LastName == "Cooper")
                .ToList();
            //players alexander or wyatt
            ViewBag.AWs = _context.Players
                .Where(l => l.FirstName == "Alexander" || l.FirstName == "Wyatt")
                .ToList();
            return View("Level1");


    
        }

        [HttpGet("level_2")]
        public IActionResult Level2()
        {
            //teams in atlantic soccer conference
            ViewBag.AtlanticSoccer = _context.Leagues
                .Include(league => league.Teams)
                .FirstOrDefault(league => league.Name == "Atlantic Soccer Conference")
                .Teams; 
            //current players in boston penguins
            ViewBag.PenguinPlayers = _context.Teams
                .Include(team => team.CurrentPlayers)
                .FirstOrDefault(team => team.TeamName == "Penguins")
                .CurrentPlayers;
            //current players in international C baseball conference
            ViewBag.PlayersICBC = _context.Players
                .Include(players=> players.CurrentTeam)
                .ThenInclude(team => team.CurrLeague)
                .Where(player => player.CurrentTeam.CurrLeague.Name == "International Collegiate Baseball Conference"); 
            //current players in american conference A Football w last name Lopez
            ViewBag.LopezPlayers = _context.Players
                .Include(players => players.CurrentTeam)
                .ThenInclude(team => team.CurrLeague)
                .Where(player => player.CurrentTeam.CurrLeague.Name == "American Conference of Amateur Football" && player.LastName == "Lopez"); 
            //all football players
             ViewBag.AllFootballPlayers = _context.Players
                .Include(players => players.CurrentTeam)
                .ThenInclude(team=> team.CurrLeague)
                .Where(player => player.CurrentTeam.CurrLeague.Sport =="Football"); 
            //all teams with current players named sophia
            ViewBag.SophiaTeams = _context.Teams
                .Include(team => team.CurrentPlayers).Where(team => team.CurrentPlayers.Any(player => player.FirstName == "Sophia")); 
            //all leagues with current players named sophia
            ViewBag.SophiaLeagues = _context.Leagues
                .Include(league => league.Teams)
                .ThenInclude(team => team.CurrentPlayers)
                .Where(league => league.Teams
                .Any(team => team.CurrentPlayers
                .Any(player => player.FirstName == "Sophia")));
            //everyone with last name flores and doesnt currently play for washington roughriders
            ViewBag.FloresPlayers = _context.Players
                .Include(players => players.CurrentTeam)
                .Where(player => player.CurrentTeam.TeamName != "Roughriders" && player.LastName == "Flores");  

            return View();
        }

        [HttpGet("level_3")]
        public IActionResult Level3()
        {
            return View();
        }

    }
}