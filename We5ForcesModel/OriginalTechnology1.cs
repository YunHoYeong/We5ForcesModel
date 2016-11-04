﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace We5ForcesModel
{
    public partial class OriginalTechnology1 : Form
    {
        public OriginalTechnology1()
        {
            InitializeComponent();
        }
        private void OriginalTechIntroduction()
        {
            // RCWS 기준
            string[][] OriginalTechnology = new string[6][];
            string[][] DescriptionTechonology = new string[6][];
            bool[][] MainTechnology = new bool[6][];

            OriginalTechnology[2] = new string[4] { "영상기술", "사격통제기술", "구동/제어기술", "운용기술" };
            DescriptionTechonology[2] = new string[4]
            {
                "- 주/야간 표적의 식별 탐지를 위한 기술\n- 주간관측장비, 열영상장비, 거리측정기(LRF), 하우징 조립체로 구성",
                "- K-4와 K-6 기관총을 장착하고, 제어장치로부터 제어입력을 통해 장전과 격발을 수행\n- 완충장비를 사용하여 사격시 충격을 완화시키는 기능",
                "- 구동장치는 구조물 조립체, 구동부 조립체, 슬립링 조립체로 구성\n- 제어장치는 구동장치를 제어하기 위한 안정화 제어기능과 외부를 측정하기 위한 자이로센서로 구성\n- 특히 기동 및 사격시 외부충격 및 진동에 대한 안정화 및 시스템 제어 성능이 요구",
                "- 영상장치로부터 획득한 영상을 전시하고, 사수가 시스템 운용 및 사격을 위한 조이스틱으로 구성"
            };
            MainTechnology[2] = new bool[4] { false, false, true, false };

            //  lbl_Introduction.Text = Introduction[2];
            metroGrid1.Rows.Clear();
            metroGrid1.RowHeadersVisible = false;
            metroGrid1.ColumnCount = 2;

            metroGrid1.Columns[0].Name = "기술명";
            metroGrid1.Columns[1].Name = "기술내용";

            // Clumn Header 변경
            // 개요
            metroGrid1.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(32, 56, 100);
            metroGrid1.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;

            // 무기체계명 
            DataGridViewColumn dataGridViewColumn = metroGrid1.Columns[0];
            dataGridViewColumn.HeaderCell.Style.BackColor = Color.FromArgb(91, 155, 213);
            dataGridViewColumn.HeaderCell.Style.ForeColor = Color.White;


            for (int i = 0; i < metroGrid1.Columns.Count; i++)
            {
                metroGrid1.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            for(int i = 0; i < OriginalTechnology[mainFrm.CurrentWeapon].Length; i++ )
            {
                metroGrid1.Rows.Add(" " + OriginalTechnology[mainFrm.CurrentWeapon][i] + " " , DescriptionTechonology[mainFrm.CurrentWeapon][i]);
            }
            
            for(int i = 0; i < metroGrid1.RowCount; i++)
            {
                metroGrid1.Rows[i].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;
            }

            // 무기체계 작성 Cell 스타일
            metroGrid1.Rows[0].Cells[0].Style.BackColor = Color.FromArgb(222, 235, 247);
            metroGrid1.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);

            metroGrid1.Rows[0].Cells[1].Style.Alignment = DataGridViewContentAlignment.MiddleLeft;

            metroGrid1.CurrentCell = null;
            for (int i = 1; i < metroGrid1.RowCount; i++)
            {
                for (int j = 0; j < metroGrid1.ColumnCount; j++)
                {
                    metroGrid1.Rows[i].Cells[j].Style.BackColor = System.Drawing.Color.White;
                }
                metroGrid1.Rows[i].Cells[0].Style.BackColor = System.Drawing.Color.FromArgb(222, 235, 247);
                metroGrid1.Rows[i].Cells[0].Style.ForeColor = System.Drawing.Color.Black;
            }

            foreach (DataGridViewColumn column in metroGrid1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
                metroGrid1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            metroGrid1.Columns[0].Width = (int)(metroGrid1.Width * 0.15);
            metroGrid1.Columns[1].Width = (int)(metroGrid1.Width * 0.84);
            //   metroGrid1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.NotSet;

            int X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            metroGrid1.Height = X + 10;

            X = metroGrid1.ColumnHeadersHeight + metroGrid1.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            lbl2.Location = new Point(metroGrid1.Location.X, X + metroGrid1.Location.Y + 20);
//            metroGrid2.Location = new Point(lbl2.Location.X, lbl2.Location.Y + 20);

        }

        private void OriginalTechnology1_Load(object sender, EventArgs e)
        {
            OriginalTechIntroduction();
        }
    }
}