﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Project.TofuGirl.Entity
{
    public class StageEntityLogic : GOEntityLogic
    {
        private StageEntityData m_EntityData;
        public Transform UpTran { get; private set; }
        public Transform DownTran { get; private set; }
        public Transform MiddleTran { get; private set; }

        protected override void OnInit(object userData)
        {
            base.OnInit(userData);
            UpTran = transform.GetChild(0);
            MiddleTran = transform.GetChild(1);
            DownTran = transform.GetChild(2);
        }

        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_EntityData = userData as StageEntityData;
            if(m_EntityData==null)
            {
                return;
            }
            float rate = (float)Screen.width / Screen.height;
            float cameraH = m_EntityData.CameraOrthographicSize / rate;
            UpTran.position = new Vector3(0, cameraH/2, 0);
            MiddleTran.position = Vector3.zero;
            DownTran.position = new Vector3(0,- cameraH / 2, 0);
        }



        public override void Pause(object userData)
        {







        }
    }
}