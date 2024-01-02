(function () {
    'use strict'
    const inputs = document.querySelectorAll('.editable')
    const btnEdit = document.querySelector('#btnEdit')
    const btnSubmitEdit = document.querySelector('#btnSubmitEdit')
    const btnSubmitDelete = document.querySelector('#btnSubmitDelete')
    const btnSave = document.getElementById('card_cta-saveChanges')
    const deletarBtn = document.getElementById('deletarBtn')
    const btnconfirmDelete = document.getElementById('confirmDelete')
    const btncancelDelete = document.getElementById('cancelDelete')
    const deleteAlert = document.getElementById('deleteAlert')
    const btnReset = document.getElementById('btnReset')


    btncancelDelete.addEventListener('click', cancelDelete)
    deletarBtn.addEventListener('click', confirmDelete);
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
        let select = document.getElementById('select_disabled')
        select.disabled = !select.disabled
        select.focus()

        inputs.forEach(function (item) {
            item.disabled = !item.disabled
        })
        ButtonsShow()
    }
    function ButtonsShow() {
        btnEdit.style.display = 'none'
        btncancelDelete.style.display = 'flex'
        btnSave.style.display = 'flex'
        deletarBtn.style.display = 'none'
    }
    function ButtonsHidden() {
        btnEdit.style.display = 'flex'
        btncancelDelete.style.display = 'none'
        btnSave.style.display = 'none'
        deletarBtn.style.display = 'flex'
    }

    function confirmDelete() {
        deleteAlert.style.display = 'block'
        deletarBtn.style.display = 'none'
        btnEdit.style.display = 'none'
        btnconfirmDelete.style.display = 'flex'
        btncancelDelete.style.display = 'flex'
    }
    function cancelDelete() {
        deleteAlert.style.display = 'none'
        deletarBtn.style.display = 'flex'
        btnEdit.style.display = 'flex'
        btnSave.style.display = 'none'
        btnconfirmDelete.style.display = 'none'
        btncancelDelete.style.display = 'none'
        disable()
        ButtonsHidden()
        btnReset.click()

    }
      
})()