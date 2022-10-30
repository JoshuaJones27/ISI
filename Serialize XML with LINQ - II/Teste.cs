/*
*	<copyright file="SerializeTo.cs" company="IPCA">
*		Copyright (c) 2021 All Rights Reserved
*	</copyright>
* 	<author>lufer</author>
*   <date>10/18/2021 10:54:30 PM</date>
*	<description></description>
**/
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SerializeTo
{
    #region Car
    /// <summary>
    /// Purpose:
    /// Created by: lufer
    /// Created on: 10/18/2021 10:54:30 PM
    /// </summary>
    /// <remarks></remarks>
    /// <example></example>
    [Serializable]
    public class Radio
    {
        public bool hasTweeters;
        public bool hasSubWoofers;
        public double[] stationPresets;
        public string radioID = "002";
    }

    [Serializable]
    public class Car
    {
        public Radio theRadio = new Radio();
        public bool isHatchBack;
    }

    [Serializable, XmlRoot(Namespace = "http://www.ipca.pt")]
    public class MyCar : Car
    {
        public MyCar() { }
        [XmlAttribute]
        public bool canFly;
        [XmlAttribute]
        public bool canSubmerge;
    }
    #endregion

    #region EMPLOYER
    public class Department
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
        public Department()
        {
            Employees = new List<Employee>();
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public Employee() { }
        public Employee(string name)
        {
            Name = name;
        }
    }
    #endregion
}
