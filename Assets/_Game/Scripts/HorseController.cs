using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HorseController : MonoBehaviour
{
    public float Id { get; set; }
    public string Name { get; set; }
    [SerializeField] protected Text textName;
    [SerializeField] protected float speed;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Animator anim;
    [SerializeField] protected SkinnedMeshRenderer sMRHorse;
    [SerializeField] protected SkinnedMeshRenderer sMRCowboy;
    [SerializeField] protected Transform holderNumber;
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float distanceRandomSpeed = 100f;
    protected Vector3 pointRandomSpeed;
    protected string animName = Constants.ANIM_IDLE;

    public bool IsCheckRandomSpeed { get; set; }
    public bool IsMoving { get; set; }

    public void OnInit(int id)
    {
        this.Id = id;

        IsCheckRandomSpeed = true;
        IsMoving = true;

        // random speed
        speed = Random.Range(minSpeed-10, maxSpeed-10);

        StartCoroutine(CoRandomSpeed());

        // spawn number by id 
        GameObject number3d = Instantiate(GameManager.Instance.soDataText.GetText3D(id), holderNumber);
        number3d.transform.localPosition = Vector3.zero;
        number3d.transform.localEulerAngles = Vector3.zero;
        number3d.transform.localScale = Vector3.one;

        ChangeAnim(Constants.ANIM_IDLE);

        // random color
        sMRHorse.materials[0].color = GameManager.Instance.randomColor.GetRandomColor();
        sMRHorse.materials[1].color = GameManager.Instance.randomColor.GetRandomColor();
        sMRCowboy.material.color = GameManager.Instance.randomColor.GetRandomColor();

        // Init position pointRandomSpeed
        pointRandomSpeed = new Vector3(transform.localPosition.x, transform.localPosition.y, distanceRandomSpeed);

        // sinh ra text 


        textName.text = Name.ToString();

        textName.color = Color.white;

        if (Name.CompareTo("Player") == 0)
        {
            textName.color = Color.green;
        }
    }
    public void OnReset()
    {

        IsCheckRandomSpeed = true;
        IsMoving = true;
        // random speed
        speed = Random.Range(minSpeed, maxSpeed);
        StartCoroutine(CoRandomSpeed());
        ChangeAnim(Constants.ANIM_IDLE);
        // Init position pointRandomSpeed
        pointRandomSpeed = new Vector3(transform.localPosition.x, transform.localPosition.y, distanceRandomSpeed);
        transform.localPosition = new Vector3(transform.localPosition.x,
                                                        transform.localPosition.y, 0);
    }

    public void ChangeAnim(string animName)
    {
        if (this.animName != animName)
        {
            anim.ResetTrigger(this.animName);
            this.animName = animName;
            anim.SetTrigger(this.animName);
        }
    }
    private void FixedUpdate()
    {
        if (IsMoving)
        {
            Move();
        }
    }
    private void Move()
    {
        if (GameManager.Instance.State != GameState.GamePlay)
        {
            ChangeAnim(Constants.ANIM_IDLE);
            return;
        }
        ChangeAnim(Constants.ANIM_RUN);
        Vector3 direction = new Vector3(0, 0, 1);
        rb.velocity = direction * speed;
    }
    private IEnumerator CoRandomSpeed()
    {
        while (IsCheckRandomSpeed)
        {
            yield return new WaitForEndOfFrame();
            if (Vector3.Distance(transform.localPosition, pointRandomSpeed) < 10f)
            {
                pointRandomSpeed = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + distanceRandomSpeed);
                speed = Random.Range(minSpeed, maxSpeed);
            }
        }
    }
    public IEnumerator SlowDownSpeed()
    {
        while (speed > 0)
        {
            yield return new WaitForEndOfFrame();
            speed -= 10f*Time.deltaTime;
        }
        if (speed <= 0)
        {
            IsMoving = false;
            speed = 0f;
            rb.velocity = Vector3.zero;
            ChangeAnim(Constants.ANIM_IDLE);
        }

    }
    public float GetDistanceFinish()
    {
        return Vector3.Distance(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, 2000f));
    }
}
