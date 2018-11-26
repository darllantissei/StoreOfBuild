var gulp = require('gulp');
var concat = require('gulp-concat');
var cssmin = require('gulp-cssmin');
var uncss = require('gulp-uncss');
var browsersync = require('browser-sync').create();

gulp.task('browser-sync', function(){
    browsersync.init({
        proxy: 'localhost:5000'
    });

    gulp.watch('./style/*.css', ['css']);
    gulp.watch('./js/*.js', ['js']);
    gulp.watch('./Fonts/*.*', ['fonts'])
});

gulp.task('js', function(){
    
    return gulp.src([
        './node_modules/bootstrap/dist/js/bootstrap.min.js',
        './node_modules/jquery/dist/jquery.min.js',
        './node_modules/jquery-validation/dist/jquery.validate.min.js',
        './node_modules/jquery-validation-unobtrusive/dist/jquery.validate.unobtrusive.js',
        './node_modules/jquery-ajax-unobtrusive/dist/jquery.unobtrusive-ajax.min.js',
        './js/site.js',
    ])
    .pipe(gulp.dest('wwwroot/js/'))
    .pipe(browsersync.stream());
});

gulp.task('css', function(){

    return gulp.src([
        './Style/site.css',
        './node_modules/bootstrap/dist/css/bootstrap.css',
    ])
    .pipe(concat('site.min.css'))
    .pipe(cssmin())
    .pipe(uncss({html: ['Views/**/*.cshtml']}))
    .pipe(gulp.dest('wwwroot/css/'))
    .pipe(browsersync.stream());
});

gulp.task('fonts', function () {
    return gulp.src([
        './Fonts/*.*'
    ])
    .pipe(gulp.dest('wwwroot/fonts/'))
    .pipe(browsersync.stream());
});