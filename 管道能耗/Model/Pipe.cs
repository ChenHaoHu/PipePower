using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Pipe
    {
        //         * CREATE TABLE [dbo].[tb_pipe] (
        //  [project_id] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [start_spot] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [end_spot] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [pipe_length] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [pipe_outer_diameter] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [wall_thickness] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [pipe_depth] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [transport_medium] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [insulation_materials] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [tank_type_a] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [tank_type_b] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [tank_capacity_a] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [tank_capacity_b] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [maximum_pressure] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [pipe_name] varchar(255) COLLATE Chinese_PRC_CI_AS  NULL,
        //  [pipe_id] int  NULL,
        //  [id] int  NOT NULL
        //)
        int projectId;
        int pipeId;
        string start;
        string end;
        string name;
        string length;
        string outerDiameter;
        string wallThickness;
        string depth;
        string trans;
        string insulation;
        string tankTypeA;
        string tankTypeB;
        string tankCapacityA;
        string tankCapacityB;
        string maximumPressure;

        public Pipe(int projectId, int pipeId, string start, string end, string name, string length, string outerDiameter, string wallThickness, string depth, string trans, string insulation, string tankTypeA, string tankTypeB, string tankCapacityA, string tankCapacityB, string maximumPressure)
        {
            this.projectId = projectId;
            this.pipeId = pipeId;
            this.start = start;
            this.end = end;
            this.name = name;
            this.length = length;
            this.outerDiameter = outerDiameter;
            this.wallThickness = wallThickness;
            this.depth = depth;
            this.trans = trans;
            this.insulation = insulation;
            this.tankTypeA = tankTypeA;
            this.tankTypeB = tankTypeB;
            this.tankCapacityA = tankCapacityA;
            this.tankCapacityB = tankCapacityB;
            this.maximumPressure = maximumPressure;
        }

        public int ProjectId { get => projectId; set => projectId = value; }
        public int PipeId { get => pipeId; set => pipeId = value; }
        public string Start { get => start; set => start = value; }
        public string End { get => end; set => end = value; }
        public string Name { get => name; set => name = value; }
        public string Length { get => length; set => length = value; }
        public string OuterDiameter { get => outerDiameter; set => outerDiameter = value; }
        public string WallThickness { get => wallThickness; set => wallThickness = value; }
        public string Depth { get => depth; set => depth = value; }
        public string Trans { get => trans; set => trans = value; }
        public string Insulation { get => insulation; set => insulation = value; }
        public string TankTypeA { get => tankTypeA; set => tankTypeA = value; }
        public string TankTypeB { get => tankTypeB; set => tankTypeB = value; }
        public string TankCapacityA { get => tankCapacityA; set => tankCapacityA = value; }
        public string TankCapacityB { get => tankCapacityB; set => tankCapacityB = value; }
        public string MaximumPressure { get => maximumPressure; set => maximumPressure = value; }
    }
}
