using System;
using System.Collections.Generic;
using DG.Tweening;
using GuyEssence;
using LineEssence;
using TMPro;
using UnityEngine;

namespace PlayerEssence
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private List<Guy> _guys;
        [SerializeField] private LineDrawer _drawer;

        [SerializeField] private Vector3 _offset;
        [SerializeField] private Vector3 _scale;

        private void Awake()
        {
            _drawer.OnLineDrew += LineDrew;
            foreach (var guy in _guys)
            {
                InitGuy(guy);
            }
        }

        private void Remove(Guy guy)
        {
            guy.OnGuyCollided -= AddNewGuy;
            guy.OnDying -= Remove;
            _guys.Remove(guy);
        }

        private void LineDrew(Line line)
        {
            Vector3[] positions = line.Separate(_guys.Count);
            for(int i = 0; i < _guys.Count; i ++)
            {
                Vector3 newPos = new Vector3(positions[i].x, 0.1f, positions[i].y);
                newPos.x *= _scale.x;
                newPos.z *= _scale.z;
                newPos += _offset;
                
                _guys[i].transform.DOLocalMove(newPos, 1);
            }
        }

        private void AddNewGuy(Guy guy)
        {
            if(_guys.Contains(guy)) return;
            guy.transform.SetParent(transform);
            guy.transform.localRotation = Quaternion.identity;
            InitGuy(guy);
            _guys.Add(guy);
        }

        private void InitGuy(Guy guy)
        {
            guy.OnGuyCollided += AddNewGuy;
            guy.OnDying += Remove;
            guy.Go();
        }
    }
}