﻿using System.ComponentModel.DataAnnotations;

namespace CricketWebApplicationMVC.Models
{
    public class AddTeamModel
    {
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public byte[] TeamLogo { get; set; }

        public string Base64String { get; set; }
        public string Player1 {  get; set; }
        public string Player2 {  get; set; }
        public string Player3 {  get; set; }
        public string Player4 {  get; set; }
        public string Player5 {  get; set; }
        public string Player6 {  get; set; }
        public string Player7 {  get; set; }
        public string Player8 {  get; set; }
        public string Player9 {  get; set; }
        public string Player10 {  get; set; }
        public string Player11 {  get; set; }
    }
}
