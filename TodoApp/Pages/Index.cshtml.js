$(function () {
    
    /*** DELETING ITEMS ***/
    $('#TodoList').on('click', 'li i.fa-trash-o', function(){
        var $li = $(this).parent();
        var id = $li.attr('data-id');

        todoApp.services.todo.delete(id).then(function(){
            $li.remove();
            abp.notify.info('Deleted the todo item.');
        });
    });

    /*** CREATING NEW ITEMS ***/
    $('#NewItemForm').submit(function(e){
        e.preventDefault();

        // Get the logged-in user ID
        var userId = $('.container').data('userid');

        var todoText = $('#NewItemText').val();
        todoApp.services.todo.create(todoText,userId).then(function(result){
            $('<li data-id="' + result.id + '">')
                .html('<i class="fa fa-trash-o"></i> <i class="fas fa-pencil-alt" style="color: green;"></i>' + result.text)
                .appendTo($('#TodoList'));
            $('#NewItemText').val('');
        });
    });

    /*** SHOW UPDATE/EDIT POPUP AND UPDATE ITEMS ***/
    $(document).ready(function () {
        $('#TodoList').on('click', 'li i.fas.fa-pencil-alt', function () {
            var $li = $(this).parent();
            var id = $li.attr('data-id');
            var currentText = $li.text().trim();

            $('#EditText').val(currentText);

            $('#editModal').modal('show');

            $('#EditTextForm').off('submit').on('submit', function (e) {
                e.preventDefault();
                var updatedText = $('#EditText').val();

                //Update the client-side representation
                $li.html('<i class="fa fa-trash-o"></i> <i class="fas fa-pencil-alt" style="color: green;"></i>' + updatedText);

                $('#editModal').modal('hide');
                abp.notify.info('Updated the todo item.');
            });
        });
    });

     /*** SLIDE SHOW ***/
    const slides = document.querySelectorAll('.slide');
    let currentSlide = 0;

    function showSlide(index) {
        slides.forEach((slide, i) => {
            if (i === index) {
                slide.classList.add('active');
            } else {
                slide.classList.remove('active');
            }
        });
    }
    function nextSlide() {
        currentSlide++;
        if (currentSlide >= slides.length) {
            currentSlide = 0;
        }
        showSlide(currentSlide);
    }

    setInterval(nextSlide, 4000);

    /*** COLOR CHANGE***/
    const titleElement = document.getElementById('shopping-list-title');

    const colors = ['lightgreen', 'lightpink', 'lightgrey', 'lightgoldenrodyellow', 'lightblue'];

    function changeTextColor() {

        const randomColor = colors[Math.floor(Math.random() * colors.length)];

        titleElement.style.color = randomColor;
    }

    setInterval(changeTextColor, 1000);
});
