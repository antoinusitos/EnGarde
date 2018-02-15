using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CardType
{
    NONE,
    ARROW,
    MAGIC,
    SWORD,
    MOVE,
    SHIELD,
}

public class Actions
{
    protected CardType _currentType = CardType.NONE;

    protected int _currentAmount = 1;
    protected int _resolutionAmount = 1;

    protected bool _canAct = true;
    protected bool _mustUpdate = false;

    protected float _timeResolution = 0.3f;
    protected float _currentTimeResolution = 0f;

    public virtual void InitAction()
    {

    }

    public virtual void ExecuteAction(int fromPlayer, Board currentBoard)
    {
        _canAct = false;
        _mustUpdate = true;
    }

    public CardType GetCardType()
    {
        return _currentType;
    }

    public int GetCardAmount()
    {
        return _currentAmount;
    }

    public void SetCardAmount(int amount)
    {
        _currentAmount = amount;
    }

    public bool GetCanAct()
    {
        return _canAct;
    }

    public void StartResolution()
    {
        _resolutionAmount = _currentAmount;
    }

    public int GetResolutionAmount()
    {
        return _resolutionAmount;
    }

    public void Update()
    {
        if (_mustUpdate)
        {
            _currentTimeResolution += Time.deltaTime;
            if (_currentTimeResolution >= _timeResolution)
            {
                StopAction();
                _resolutionAmount--;
                if (_resolutionAmount > 0)
                    _canAct = true;
            }
        }
    }

    public void StopAction()
    {
        _currentTimeResolution = 0;
        _mustUpdate = false;
        _canAct = true;
    }
}
