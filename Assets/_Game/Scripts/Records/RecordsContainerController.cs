using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecordsContainerController : MonoBehaviour
{
    [Header("Record Trigger Controllers")]
    [SerializeField] RecordTriggerController _recordTriggerControllerA;
    [SerializeField] RecordTriggerController _recordTriggerControllerB;
    [SerializeField] RecordTriggerController _recordTriggerControllerC;
    [SerializeField] RecordTriggerController _recordTriggerControllerD;
    [SerializeField] RecordTriggerController _recordTriggerControllerE;
    [SerializeField] RecordTriggerController _recordTriggerControllerF;
    [SerializeField] RecordTriggerController _recordTriggerControllerG;
    [SerializeField] RecordTriggerController _recordTriggerControllerH;
    [SerializeField] RecordTriggerController _recordTriggerControllerI;
    [SerializeField] RecordTriggerController _recordTriggerControllerJ;

    [Header("Records Found")]
    [SerializeField] BoolVariable _recordAFound;
    [SerializeField] BoolVariable _recordBFound;
    [SerializeField] BoolVariable _recordCFound;
    [SerializeField] BoolVariable _recordDFound;
    [SerializeField] BoolVariable _recordEFound;
    [SerializeField] BoolVariable _recordFFound;
    [SerializeField] BoolVariable _recordGFound;
    [SerializeField] BoolVariable _recordHFound;
    [SerializeField] BoolVariable _recordIFound;
    [SerializeField] BoolVariable _recordJFound;


    void OnEnable()
    {
        _recordTriggerControllerA.gameObject.SetActive(_recordAFound.Value);
        _recordTriggerControllerB.gameObject.SetActive(_recordBFound.Value);
        _recordTriggerControllerC.gameObject.SetActive(_recordCFound.Value);
        _recordTriggerControllerD.gameObject.SetActive(_recordDFound.Value);
        _recordTriggerControllerE.gameObject.SetActive(_recordEFound.Value);
        _recordTriggerControllerF.gameObject.SetActive(_recordFFound.Value);
        _recordTriggerControllerG.gameObject.SetActive(_recordGFound.Value);
        _recordTriggerControllerH.gameObject.SetActive(_recordHFound.Value);
        _recordTriggerControllerI.gameObject.SetActive(_recordIFound.Value);
        _recordTriggerControllerJ.gameObject.SetActive(_recordJFound.Value);
    }


}
