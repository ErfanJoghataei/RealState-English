

// اسکریپت برای modal اطلاعات تماس
const contactModal = document.getElementById('contact-modal');
const showContactBtn = document.getElementById('show-contact');
const showContactBottomBtn = document.getElementById('show-contact-bottom');
const closeModalBtn = document.getElementById('close-modal');

showContactBtn.addEventListener('click', function () {
    contactModal.style.display = 'flex';
});

showContactBottomBtn.addEventListener('click', function () {
    contactModal.style.display = 'flex';
});

closeModalBtn.addEventListener('click', function () {
    contactModal.style.display = 'none';
});

// بستن modal با کلیک خارج از آن
window.addEventListener('click', function (event) {
    if (event.target === contactModal) {
        contactModal.style.display = 'none';
    }
});
function scrollToBottom() {
    const target = document.documentElement.scrollHeight; // انتهای صفحه
    const start = window.scrollY; // موقعیت فعلی
    const distance = target - start;
    const duration = 2000; // مدت زمان (۲ ثانیه)
    let startTime = null;

    function animation(currentTime) {
        if (!startTime) startTime = currentTime;
        const elapsed = currentTime - startTime;
        const progress = Math.min(elapsed / duration, 1); // از 0 تا 1
        const ease = progress * (2 - progress); // easing نرم

        window.scrollTo(0, start + distance * ease);

        if (progress < 1) {
            requestAnimationFrame(animation);
        }
    }

    requestAnimationFrame(animation);
}
document.addEventListener("DOMContentLoaded", function () {
    const mainImage = document.getElementById("main-image");
    const thumbs = document.querySelectorAll(".gallery-thumbs .gallery-thumb");

    thumbs.forEach(thumb => {
        thumb.addEventListener("click", function () {
            const newSrc = thumb.getAttribute("data-image");

            // Fade out
            mainImage.style.opacity = 0;

            // وقتی تصویر جدید کامل لود شد، opacity را دوباره برگردان
            mainImage.onload = function () {
                mainImage.style.opacity = 1;
            };

            mainImage.src = newSrc;

            // آپدیت کلاس active برای thumbnail ها
            thumbs.forEach(t => t.classList.remove("active"));
            thumb.classList.add("active");
        });
    });
});

