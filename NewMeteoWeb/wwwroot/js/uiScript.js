$(".signUp").hide();

$(".navbar-item").click((x) => {
  if (x.target.firstElementChild !== null) {
    if (x.target.firstElementChild.className === "submenu") {
      $(x.target.firstElementChild).slideToggle(300);
    }
  }
});

$(".window-back").click((x) => {
  $(".window-back").slideToggle(300);
  $(".window").slideToggle(300);
});

$(".changeSignType").click((x) => {
  $(".signIn").slideToggle(300);
  $(".signUp").slideToggle(300);
});

function openFromUser(x) {
    let canvas = $(".canvas");
    if (canvas !== null) {
        let photo = x.files[0];
        let image = document.getElementById('image');
        image.src = URL.createObjectURL(photo);
    }
}

function showGetMapWindow() {
  $(".submenu").hide();
  $(".window-back").slideToggle(300);
  $(".getMap-window").slideToggle(300);
}

