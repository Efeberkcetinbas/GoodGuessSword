using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RivalArmy : MonoBehaviour
{
    private FormationBase _formation;

    public RivalData rivalData;
    public PlayerData playerData;
    [SerializeField] private RadialFormation radialFormation;
    [SerializeField] private GameObject deadEffect;

    public FormationBase Formation {
        get {
            if (_formation == null) _formation = GetComponent<FormationBase>();
            return _formation;
        }
        set => _formation = value;
    }

    [SerializeField] private GameObject _unitPrefab;
    [SerializeField] private float _unitSpeed = 2;

    private readonly List<GameObject> _spawnedUnits = new List<GameObject>();
    private List<Vector3> _points = new List<Vector3>();
    private Transform _parent;

    private void Awake() {
        _parent = new GameObject("Unit Rival Parent").transform;
    }

    /*private void Update() {
        SetFormation();
    }*/

    private void OnEnable() 
    {
        EventManager.AddHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.AddHandler(GameEvent.OnUpdateRivalArmy,OnUpdateRivalArmy);
        
    }

    private void OnDisable() 
    {
        EventManager.RemoveHandler(GameEvent.OnTakeRivalDamage,OnTakeRivalDamage);
        EventManager.RemoveHandler(GameEvent.OnUpdateRivalArmy,OnUpdateRivalArmy);
    }

    private void OnUpdateRivalArmy()
    {
        SetFormation();
    }

    private void OnTakeRivalDamage()
    {
        Kill(playerData.MaxDamageAmount);
        //SetFormation();
    }

    private void Start() {
        SetFormation();
    }

    private void SetFormation() {
        radialFormation._amount=rivalData.TempHealth;
        _points = Formation.EvaluatePoints().ToList();

        if (_points.Count > _spawnedUnits.Count) {
            var remainingPoints = _points.Skip(_spawnedUnits.Count);
            Spawn(remainingPoints);
        }
        else if (_points.Count < _spawnedUnits.Count) {
            Kill(_spawnedUnits.Count - _points.Count);
        }

        for (var i = 0; i < _spawnedUnits.Count; i++) {
            _spawnedUnits[i].transform.position = Vector3.MoveTowards(_spawnedUnits[i].transform.position, transform.position + _points[i], _unitSpeed * Time.deltaTime);
        }
    }

    

    private void Spawn(IEnumerable<Vector3> points) {
        foreach (var pos in points) {
            var unit = Instantiate(_unitPrefab, transform.position + pos, Quaternion.identity, _parent);
            _spawnedUnits.Add(unit);
        }
    }

    private void Kill(int num) {
        for (var i = 0; i < num; i++) {
            var unit = _spawnedUnits.Last();
            Instantiate(deadEffect,unit.transform.position,unit.transform.rotation);
            _spawnedUnits.Remove(unit);
            Destroy(unit.gameObject);
        }
    }
}
