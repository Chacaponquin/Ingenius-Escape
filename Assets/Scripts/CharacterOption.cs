using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterOption : MonoBehaviour
{
    public FormController form;
    public CHARACTER_TYPE type;

    private void OnMouseDown()
    {
        int existsInFirstList = this.form.list1.IndexOf(gameObject);


        if(existsInFirstList != -1 && this.form.turnPlayer == 0)
        {
            if (this.form.listPlayer1.Count < 5)
            {
                this.form.listPlayer1.Add(this.type);
                GameObject obj = Instantiate<GameObject>(this.gameObject, form.listSelectedPlayer1[form.indexT1].transform.position, Quaternion.identity);
                obj.transform.localScale = new Vector3(2, 2, 2);
                form.indexT1++;

                GameObject found = null;
                for(int i =0; i < this.form.list2.Count && found == null; i++)
                {
                    CharacterOption a = this.form.list2[i].GetComponent<CharacterOption>();

                    if (a.type == this.type)
                    {
                        found = this.form.list2[i];
                    }
                }

                if(found != null)
                {
                    found.SetActive(false);
                }

                this.form.turnPlayer = this.form.turnPlayer == 0 ? 1 : 0;
            }
        }

        else if(this.form.turnPlayer == 1 && existsInFirstList == -1)
        {
            if (this.form.listPlayer2.Count < 5 )
            {
                this.form.listPlayer2.Add(this.type);
                GameObject obj = Instantiate<GameObject>(this.gameObject, form.listSelectedPlayer2[form.indexT2].transform.position, Quaternion.identity);
                obj.transform.localScale = new Vector3(2, 2, 2);
                form.indexT2++;

                GameObject found = null;
                for (int i = 0; i < this.form.list1.Count && found == null; i++)
                {
                    CharacterOption a = this.form.list1[i].GetComponent<CharacterOption>();

                    if (a.type == this.type)
                    {
                        found = this.form.list1[i];
                    }
                }

                if (found != null)
                {
                    found.SetActive(false);
                }

                this.form.turnPlayer = this.form.turnPlayer == 0 ? 1 : 0;
            }
        }
    }
}
