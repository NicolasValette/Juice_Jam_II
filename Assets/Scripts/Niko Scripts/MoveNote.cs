using UnityEngine;
using DG.Tweening;
public class MoveNote : MonoBehaviour
{
    public float BeatOfNote;
    private float timer = 0f;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        BeatOfNote = RythmManager.Instance._previousBeat + 2f;
        transform.DOMove(RythmManager.Instance.RemoveNotePos.position, (RythmManager.Instance.BeatsShown * RythmManager.Instance._secondePerBeat) * 2);
        anim.SetFloat("Speed", 1f);
        //Debug.Log("~~~~~~~~~~~~~~~~~~~~~~~~~~~~Spawn : " + gameObject.transform.position);
    }
    // Update is called once per frame
    void Update()
    {
        //timer += Time.deltaTime;
        //float delta = (RythmManager.Instance.BeatsShown - ((BeatsOfNote - RythmManager.Instance._songPositionInBeat) / RythmManager.Instance.BeatsShown));
        
        //Debug.Log("New Frame ================");
        //Debug.Log("Beats shown : " + RythmManager.Instance.BeatsShown);
        //Debug.Log("BeatsOfNote : " + BeatsOfNote);
        //Debug.Log("_songPositionInBeat : " + RythmManager.Instance._songPositionInBeat);
        //Debug.Log("delta : " + delta);
        //Debug.Log("Timer : " + timer);
        //Debug.Log("END Frame ######################");
        //transform.position = Vector3.Lerp(RythmManager.Instance.SpawnNotePos.position, RythmManager.Instance.RemoveNotePos.position, delta);
       // transform.position = Vector3.Lerp(RythmManager.Instance.SpawnNotePos.position, RythmManager.Instance.RemoveNotePos.position,timer);
       if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<ParticleSystem>().Play();
        }
    }
    public void Miss()
    {
        Destroy(gameObject);
    }
    public void Hit()
    {
        Debug.Log("BOUM");
        ParticleSystem ps = GetComponent<ParticleSystem>();
        ps.Play();
        //gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        
        anim.SetBool("Dead", true);
        Destroy(gameObject, ps.main.duration);

    }
}
