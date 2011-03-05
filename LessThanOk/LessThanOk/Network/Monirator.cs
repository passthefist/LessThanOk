using System;
using System.Collections;
using LessThanOk.Network;

public class Monirator
{
    private Queue grants;
    private Queue requests;

	public Monirator()
	{
        grants = new Queue(100);
        requests = new Queue(100);
	}

    public void grant(Command n_command){ grants.Enqueue(n_command); }

    public void proccessReq()
    {
        Command nextReq;
        while (requests.Count != 0)
        {
            nextReq = requests.Dequeue();
            if(checkRequest(nextReq))
            {
                //grant
            }
            else
            {
                //deney
            }

        }
    }

    
}
