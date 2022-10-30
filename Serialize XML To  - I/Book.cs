/*
*	<copyright file="SerializeTo.cs" company="IPCA">
*		Copyright (c) 2021 All Rights Reserved
*	</copyright>
* 	<author>lufer</author>
*   <date>10/19/2021 5:22:16 PM</date>
*	<description></description>
**/
using System;
using Newtonsoft.Json;

namespace SerializeTo
{
    /// <summary>
    /// Purpose:
    /// Created by: lufer
    /// Created on: 10/19/2021 5:22:16 PM
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>
    class Author
    {
        [JsonProperty("id")] // Set the variable below to represent the json attribute 
        public int id;       //"id"
        [JsonProperty("name")]
        public string name;
        [JsonProperty("type")]
        public string type;
        [JsonProperty("books")]
        public Book[] books;

        public Author(int id, string name, string type, Book[] books)
        {
            this.id = id;
            this.name = name;
            this.type = type;
            this.books = books;
        }
    }

    class Book
    {
        [JsonProperty("name")]
        public string name;
        [JsonProperty("date")]
        public DateTime date;
    }
}
