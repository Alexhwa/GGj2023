using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class PlayerVisuals : MonoBehaviour
{
    [SerializeField] private DOTweenAnimation animation;
    [SerializeField] private MeshRenderer body;
    [SerializeField] private MeshRenderer black;
    [SerializeField] private MeshRenderer ears;
    [SerializeField] private MeshRenderer face;
    [SerializeField] private MeshRenderer logo;
    [SerializeField] private MeshRenderer sides;

    [SerializeField] private Material faceMat;
    [SerializeField] private Material bodyMat;
    [SerializeField] private Material hurtFaceMat;
    [SerializeField] private Material hurtBodyMat;
    
    
    public void DoHurt()
    {
        StartCoroutine(HurtSequence());
    }

    private IEnumerator HurtSequence()
    {
        animation.DOPlay();
        body.material = hurtBodyMat;
        black.material = hurtBodyMat;
        ears.material = hurtBodyMat;
        face.material = hurtFaceMat;
        logo.material = hurtBodyMat;
        sides.material = hurtBodyMat;
        yield return new WaitForSeconds(.75f);
        SetToNormal();
    }

    public void SetToNormal()
    {
        body.material = bodyMat;
        black.material = bodyMat;
        ears.material = bodyMat;
        face.material = faceMat;
        logo.material = bodyMat;
        sides.material = bodyMat;
    }
}
