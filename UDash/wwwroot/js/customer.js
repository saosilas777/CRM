(function () {
    'use strict'
    const inputs = document.querySelectorAll('.editable')

    const btnEdit = document.querySelector('#btnEdit')

    const editOrDelete = document.querySelector('#editOrDelete')
    const cancelOrSave = document.querySelector('#cancelOrSave')

    const btnEditCancelEdit = document.getElementById('cancelEdit')
    btnEditCancelEdit.addEventListener('click', ButtonsShow)

    const deletarBtn = document.getElementById('deletarBtn')
    deletarBtn.addEventListener('click', confirmDelete);

    const cancelDeleteBtn = document.getElementById('cancelDelete')
    cancelDeleteBtn.addEventListener('click', cancelDelete)


    const deleteConfirm = document.getElementById('deleteConfirm')

    const btnconfirmDelete = document.getElementById('confirmDelete')

    const btnSubmitEdit = document.querySelector('#btnSubmitEdit')
    const btnSubmitDelete = document.querySelector('#btnSubmitDelete')
    const btnSave = document.getElementById('card_cta-saveChanges')
    const deleteAlert = document.getElementById('deleteAlertContainer')
   


    btnEdit.addEventListener('click', disable)

    btnSave.addEventListener('click', SubmitEdit)
    btnconfirmDelete.addEventListener('click', SubmitDelete)


    function SubmitEdit () {
        btnSubmitEdit.click();
    }
    function SubmitDelete() {
        btnSubmitDelete.click();
    }

    function disable() {
        inputs.forEach(function (item) {
            item.disabled = !item.disabled
        })
        ButtonsHidden()
    }



    function ButtonsShow() {
        disable()
        editOrDelete.style.display = 'flex'
        cancelOrSave.style.display = 'none'
    }
    function ButtonsHidden() {
        editOrDelete.style.display = 'none'
        cancelOrSave.style.display = 'block'

    }

    function confirmDelete() {
        editOrDelete.style.display = 'none'
        deleteConfirm.style.display = 'flex'
        deleteAlert.style.display = 'flex'
       
    }

    function cancelDelete() {
        deleteAlert.style.display = 'none'
        editOrDelete.style.display = 'flex'
        deleteConfirm.style.display = 'none'
    }
      
})()