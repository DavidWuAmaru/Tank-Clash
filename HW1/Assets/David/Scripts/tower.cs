using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tower : MonoBehaviour
{
    private bool aim = false;
    private float move_center = 0.5f;
    private float add_angle = 0;

    AudioSource shootAudio;
    public GameObject missile;
    public GameObject plane;
    public Transform muzzleTransform;
    private GameObject camera;
    private bool first_person = false;

    // Start is called before the first frame update
    private void tower_rotate(float angle)
    {
        this.gameObject.transform.localRotation = Quaternion.Euler(0, angle, 0);
    }
    private void focus()
    {
        camera.GetComponent<CameraFollow>().focus();
    }
    private void unfocus()
    {
        camera.GetComponent<CameraFollow>().unfocus();
    }
    void Start()
    {
        camera = GameObject.Find("Main Camera");
        shootAudio = GetComponent<AudioSource>();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!aim)
            {
                focus();
                float first_mouse_position = Input.mousePosition.x;
                float screen_width = Screen.width;
                move_center = first_mouse_position / screen_width;
                aim = true;
            }
            else
            {
                unfocus();
                this.gameObject.transform.localRotation = Quaternion.Euler(0, 0, 0);
                add_angle = 0;
                aim = false;
            }
        }
        if (aim)
        {
            float mouse_position = Input.mousePosition.x;
            float screen_width = Screen.width;
            float move = mouse_position / screen_width;
            if (move > 0.9)
            {
                add_angle += 0.5f;
            }
            else if (move < 0.1)
            {
                add_angle -= 0.5f;
            }
            float angle = -(move_center - move) * 100 + add_angle;
            tower_rotate(angle);

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 missile_angle = transform.GetChild(0).transform.eulerAngles;
                Instantiate(missile, muzzleTransform.position, Quaternion.Euler(missile_angle));
                if (shootAudio != null)
                {
                    Debug.Log("shootAudio");
                    shootAudio.Play();
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.Q) && aim)
        {
            Vector3 offset = new Vector3(0, 5, 0);
            Instantiate(plane, transform.position + offset, Quaternion.Euler(transform.eulerAngles));
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (!first_person)
            {
                first_person = true;
                camera.GetComponent<CameraFollow>().first_person = true;
                camera.GetComponent<CameraFollow>().unfocus();
            }
            else
            {
                first_person = false;
                camera.GetComponent<CameraFollow>().first_person = false;
                camera.GetComponent<CameraFollow>().unfocus();
            }
        }
    }
}
