// Mobile menu functionality
const hamburger = document.getElementById('hamburger');
const navRight = document.querySelector('.nav-right');
const mobileMenu = document.getElementById('mobile-menu');

if (hamburger && (navRight || mobileMenu)) {
    const menu = navRight || mobileMenu;
    menu.id = menu.id || 'mobile-menu';

    hamburger.addEventListener('click', () => {
        const isOpen = menu.classList.toggle('nav-mobile-active');
        hamburger.classList.toggle('active');
        hamburger.setAttribute('aria-expanded', String(isOpen));
        document.body.style.overflow = isOpen ? 'hidden' : '';
    });

    menu.querySelectorAll('a, button').forEach((el) => {
        el.addEventListener('click', () => {
            menu.classList.remove('nav-mobile-active');
            hamburger.classList.remove('active');
            hamburger.setAttribute('aria-expanded', 'false');
            document.body.style.overflow = '';
        });
    });

    // Close on click outside
    document.addEventListener('click', (e) => {
        if (!hamburger.contains(e.target) && !menu.contains(e.target)) {
            menu.classList.remove('nav-mobile-active');
            hamburger.classList.remove('active');
            hamburger.setAttribute('aria-expanded', 'false');
            document.body.style.overflow = '';
        }
    });
}

// اسکریپت برای فیلترهای جستجو
document.addEventListener('DOMContentLoaded', function () {
    const provinceSelect = document.getElementById('province');
    const citySelect = document.getElementById('city');
    const districtSelect = document.getElementById('district');

    if (!provinceSelect || !citySelect || !districtSelect) {
        return;
    }

    const locations = {
        'تهران': {
            cities: {
                'تهران': ['نارمک', 'اقدسیه', 'پاسداران', 'ونک', 'سعادت آباد', 'شهرک غرب', 'جردن', 'یوسف آباد', 'قیطریه', 'فردوس']
            }
        },
        'خراسان رضوی': {
            cities: {
                'مشهد': ['احمدآباد', 'سجاد', 'وکیل آباد', 'قاسم آباد', 'الهیه', 'هاشمیه'],
                'نیشابور': ['مرکزی', 'باغرود', 'فرهنگیان'],
                'سبزوار': ['مرکزی', 'کاشفی', 'توحید شهر']
            }
        },
        'اصفهان': {
            cities: {
                'اصفهان': ['مرداویج', 'جلفا', 'نظر', 'سپاهان شهر', 'خانه اصفهان', 'باغ غدیر'],
                'کاشان': ['مرکزی', 'فین', 'امیرکبیر'],
                'نجف آباد': ['مرکزی', 'یزدانشهر', 'ویلاشهر']
            }
        },
        'فارس': {
            cities: {
                'شیراز': ['معالی آباد', 'قصرالدشت', 'فرهنگ شهر', 'ستارخان', 'عفیف آباد', 'قدوسی غربی'],
                'مرودشت': ['مرکزی', 'فرهنگیان', 'تخت جمشید'],
                'فسا': ['مرکزی', 'میانشهر', 'زهرا']
            }
        },
        'آذربایجان شرقی': {
            cities: {
                'تبریز': ['ولیعصر', 'ائل گلی', 'رشدیه', 'زعفرانیه', 'منظریه', 'آبرسان'],
                'مراغه': ['مرکزی', 'گلشهر', 'رضوان'],
                'مرند': ['مرکزی', 'یام', 'کوی ولیعصر']
            }
        },
        'مازندران': {
            cities: {
                'ساری': ['مرکزی', 'فرح آباد', 'شهبند'],
                'آمل': ['مرکزی', 'هراز', 'امامزاده عبدالله'],
                'بابل': ['مرکزی', 'باغ فردوس', 'شهرک اندیشه']
            }
        },
        'کرمان': {
            cities: {
                'کرمان': ['مرکزی', 'هفت باغ', 'سجاد', 'مطهری'],
                'رفسنجان': ['مرکزی', 'فرهنگیان', 'کمربندی'],
                'سیرجان': ['مرکزی', 'بلوار هجرت', 'شهرک امیرکبیر']
            }
        }
    };

    updateCities(citySelect.dataset.selected);
    updateDistricts(districtSelect.dataset.selected);

    provinceSelect.addEventListener('change', function () {
        updateCities();
        updateDistricts();
    });

    citySelect.addEventListener('change', function () {
        updateDistricts();
    });

    function updateCities(selectedCity) {
        const province = provinceSelect.value;
        const cities = locations[province]?.cities ?? {};

        citySelect.innerHTML = '<option value="">همه شهرها</option>';
        Object.keys(cities).forEach(function (city) {
            addOption(citySelect, city, city, city === selectedCity);
        });

        citySelect.disabled = Object.keys(cities).length === 0;
    }

    function updateDistricts(selectedDistrict) {
        const province = provinceSelect.value;
        const city = citySelect.value;
        const districts = locations[province]?.cities?.[city] ?? [];

        districtSelect.innerHTML = '<option value="">همه محله ها</option>';
        districts.forEach(function (district) {
            addOption(districtSelect, district, district, district === selectedDistrict);
        });

        districtSelect.disabled = districts.length === 0;
    }

    // تابع کمکی برای افزودن گزینه به select
    function addOption(selectElement, value, text, selected) {
        const option = document.createElement('option');
        option.value = value;
        option.textContent = text;
        option.selected = Boolean(selected);
        selectElement.appendChild(option);
    }
});

