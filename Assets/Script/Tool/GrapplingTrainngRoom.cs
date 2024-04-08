using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GrapplingTrainngRoom : MonoBehaviour
{
    private float _max_dist = 0f;
    [SerializeField]
    private TMP_Text _point_text;
    [SerializeField]
    private Transform _player_start_pos;
    [SerializeField]
    private Transform _start_pos;
    [SerializeField]
    private Transform _end_pos;
    [SerializeField]
    private GrapplingTrainingTriger triger;
    

    // Start is called before the first frame update
    void Start()
    {
        _max_dist = Vector3.Distance(_end_pos.position, _start_pos.position); 
        _point_text.text = "0 POINT";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!triger.is_trigger) return;
        if (other.gameObject.tag.Equals("Player"))
        {
            triger.is_trigger = false;
            float currer_dist = Vector3.Distance(new Vector3(_start_pos.position.x,
                _start_pos.position.y,
                other.transform.position.z), _start_pos.position);
            _point_text.text = ((int)( currer_dist/ _max_dist*100f)).ToString() + " POINT";
            position_init(other.gameObject);
        }
    }

    private async void position_init(GameObject player)
    {
        await System.Threading.Tasks.Task.Delay(1000);
        player.SetActive(false);
        player.transform.position = _player_start_pos.position;
        player.transform.rotation = _player_start_pos.rotation;
        player.SetActive(true);

    }
}
